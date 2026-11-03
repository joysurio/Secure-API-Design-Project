using LFS.Shared.Entities;
using LFS.SecureAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LFS.SecureAPI.Services
{
    /// <summary>
    /// User authentication via email + password. Issues JWT with auth_type=user claim.
    /// </summary>
    public class UserAuthService : IUserAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IRefreshTokenStore _refreshTokenStore;
        private readonly IApplicationUsersAdminService _applicationUsersAdmin;

        public UserAuthService(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IConfiguration configuration,
            IRefreshTokenStore refreshTokenStore,
            IApplicationUsersAdminService applicationUsersAdmin)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _refreshTokenStore = refreshTokenStore;
            _applicationUsersAdmin = applicationUsersAdmin;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email).ConfigureAwait(false);
            if (user == null)
                throw new UnauthorizedAccessException("Invalid email or password");

            if (user.Deleted)
                throw new UnauthorizedAccessException("Account is disabled");

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: false).ConfigureAwait(false);
            if (!result.Succeeded)
                throw new UnauthorizedAccessException("Invalid email or password");

            var listedInApplicationUsers = await _applicationUsersAdmin
                .IsListedInApplicationUsersAsync(user.DNI, user.IdEmployee)
                .ConfigureAwait(false);

            var token = await GenerateUserJwtAsync(user, listedInApplicationUsers).ConfigureAwait(false);
            var refreshTtl = JwtSessionLifetime.GetRefreshTokenTtl(_configuration);
            var refreshToken = _refreshTokenStore.Issue(user.Id, refreshTtl);

            var identityRoles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            var isAdmin = listedInApplicationUsers
                || identityRoles.Any(r => string.Equals(r, "admin", StringComparison.OrdinalIgnoreCase));

            return new LoginResponse
            {
                AccessToken = token,
                RefreshToken = refreshToken,
                ExpiresIn = JwtSessionLifetime.GetMobileExpiresInSeconds(_configuration),
                Role = isAdmin ? "admin" : "user"
            };
        }

        private async Task<string> GenerateUserJwtAsync(AppUser user, bool listedInApplicationUsers)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim("sub", user.Id),
                new Claim("auth_type", "user"),
                new Claim("email", user.Email ?? ""),
                new Claim("name", user.Name ?? ""),
                new Claim("jti", Guid.NewGuid().ToString())
            };

            var roles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            if (listedInApplicationUsers
                && !roles.Any(r => string.Equals(r, "admin", StringComparison.OrdinalIgnoreCase)))
                claims.Add(new Claim(ClaimTypes.Role, "admin"));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: JwtSessionLifetime.GetMobileAccessTokenExpiresUtc(_configuration),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
