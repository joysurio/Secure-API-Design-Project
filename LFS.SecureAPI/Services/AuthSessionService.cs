using LFS.InfraStructure;
using LFS.SecureAPI.Models;
using LFS.Shared.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LFS.SecureAPI.Services
{
    public class AuthSessionService : IAuthSessionService
    {
        private readonly DataContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IRefreshTokenStore _refreshTokenStore;
        private readonly IApplicationUsersAdminService _applicationUsersAdmin;

        public AuthSessionService(
            DataContext context,
            UserManager<AppUser> userManager,
            IConfiguration configuration,
            IRefreshTokenStore refreshTokenStore,
            IApplicationUsersAdminService applicationUsersAdmin)
        {
            _context = context;
            _userManager = userManager;
            _configuration = configuration;
            _refreshTokenStore = refreshTokenStore;
            _applicationUsersAdmin = applicationUsersAdmin;
        }

        public async Task<LoginResponse> RefreshAsync(string refreshToken)
        {
            var tokenTtl = JwtSessionLifetime.GetRefreshTokenTtl(_configuration);
            if (!_refreshTokenStore.Rotate(refreshToken, tokenTtl, out var newRefreshToken, out var subject))
                throw new UnauthorizedAccessException("Invalid or expired refresh token");

            // QR-admin user (paired via Users/AppUser id).
            if (TryParseQrAdminUserId(subject, out var qrAdminUserId))
            {
                var adminUser = await _userManager.Users
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.Id == qrAdminUserId && !u.Deleted)
                    .ConfigureAwait(false);
                string fullName;
                string email;
                if (adminUser != null)
                {
                    fullName = string.Join(" ", new[] { adminUser.Name, adminUser.SurName1, adminUser.SurName2 }
                        .Where(s => !string.IsNullOrWhiteSpace(s)));
                    email = adminUser.Email ?? "";
                }
                else
                {
                    var usersRow = await FindUsersTableRowByIdAsync(qrAdminUserId).ConfigureAwait(false);
                    if (usersRow == null)
                        throw new UnauthorizedAccessException("Admin user not found");
                    fullName = string.Join(" ", new[] { usersRow.Value.Name, usersRow.Value.SurName1, usersRow.Value.SurName2 }
                        .Where(s => !string.IsNullOrWhiteSpace(s)));
                    email = "";
                }

                var adminClaims = new List<Claim>
                {
                    new Claim("sub", $"qa:{qrAdminUserId}"),
                    new Claim("auth_type", "admin_user"),
                    new Claim("email", email),
                    new Claim("name", fullName ?? ""),
                    new Claim("jti", Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, "admin")
                };

                return new LoginResponse
                {
                    AccessToken = GenerateJwt(adminClaims),
                    RefreshToken = newRefreshToken,
                    ExpiresIn = JwtSessionLifetime.GetMobileExpiresInSeconds(_configuration),
                    Role = "admin"
                };
            }

            if (TryParseSubClienteId(subject, out var subClienteId))
            {
                var slice = await _context.MasterSubClientes
                    .AsNoTracking()
                    .Where(s => s.Id == subClienteId && s.Borrado != true)
                    .Select(s => new SubClienteAuthSlice(s.Id, s.Nombre, s.Apellido1, s.Dni, s.IdEmpleado))
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);
                if (slice == null)
                    throw new UnauthorizedAccessException("SubCliente account not found");

                var idEmp = ApplicationUsersAdminService.ParseEmployeeId(slice.IdEmpleado);
                var isAdmin = await _applicationUsersAdmin
                    .IsListedInApplicationUsersAsync(slice.Dni, idEmp)
                    .ConfigureAwait(false);

                var claims = new List<Claim>
                {
                    new Claim("sub", $"sc:{slice.Id}"),
                    new Claim("auth_type", "user"),
                    new Claim("subcliente_id", slice.Id.ToString()),
                    new Claim("name", string.Join(" ", new[] { slice.Nombre, slice.Apellido1 }.Where(s => !string.IsNullOrWhiteSpace(s)))),
                    new Claim("jti", Guid.NewGuid().ToString())
                };
                if (isAdmin)
                    claims.Add(new Claim(ClaimTypes.Role, "admin"));

                var accessToken = GenerateJwt(claims);
                return new LoginResponse
                {
                    AccessToken = accessToken,
                    RefreshToken = newRefreshToken,
                    ExpiresIn = JwtSessionLifetime.GetMobileExpiresInSeconds(_configuration),
                    Role = isAdmin ? "admin" : "user"
                };
            }

            var user = await _userManager.FindByIdAsync(subject).ConfigureAwait(false);
            if (user == null || user.Deleted)
                throw new UnauthorizedAccessException("User not found");

            var listed = await _applicationUsersAdmin
                .IsListedInApplicationUsersAsync(user.DNI, user.IdEmployee)
                .ConfigureAwait(false);

            var claimsApp = new List<Claim>
            {
                new Claim("sub", user.Id),
                new Claim("auth_type", "user"),
                new Claim("email", user.Email ?? ""),
                new Claim("name", user.Name ?? ""),
                new Claim("jti", Guid.NewGuid().ToString())
            };

            var roles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            foreach (var role in roles)
                claimsApp.Add(new Claim(ClaimTypes.Role, role));

            if (listed && !roles.Any(r => string.Equals(r, "admin", StringComparison.OrdinalIgnoreCase)))
                claimsApp.Add(new Claim(ClaimTypes.Role, "admin"));

            var isAppAdmin = listed
                || roles.Any(r => string.Equals(r, "admin", StringComparison.OrdinalIgnoreCase));

            var accessTokenApp = GenerateJwt(claimsApp);
            return new LoginResponse
            {
                AccessToken = accessTokenApp,
                RefreshToken = newRefreshToken,
                ExpiresIn = JwtSessionLifetime.GetMobileExpiresInSeconds(_configuration),
                Role = isAppAdmin ? "admin" : "user"
            };
        }

        public Task LogoutAsync(string refreshToken)
        {
            _refreshTokenStore.Revoke(refreshToken);
            return Task.CompletedTask;
        }

        private string GenerateJwt(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: JwtSessionLifetime.GetMobileAccessTokenExpiresUtc(_configuration),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static bool TryParseSubClienteId(string subject, out int subClienteId)
        {
            subClienteId = 0;
            return subject.StartsWith("sc:", StringComparison.Ordinal)
                && int.TryParse(subject[3..], out subClienteId);
        }

        private static bool TryParseApplicationUserId(string subject, out int applicationUserId)
        {
            applicationUserId = 0;
            return subject.StartsWith("au:", StringComparison.Ordinal)
                && int.TryParse(subject[3..], out applicationUserId);
        }

        private static bool TryParseQrAdminUserId(string subject, out string appUserId)
        {
            appUserId = "";
            if (string.IsNullOrEmpty(subject) || !subject.StartsWith("qa:", StringComparison.Ordinal))
                return false;
            appUserId = subject[3..];
            return !string.IsNullOrWhiteSpace(appUserId);
        }

        private async Task<(string Id, string? Name, string? SurName1, string? SurName2)?> FindUsersTableRowByIdAsync(string id)
        {
            var conn = _context.Database.GetDbConnection();
            var wasOpen = conn.State == System.Data.ConnectionState.Open;
            if (!wasOpen)
                await conn.OpenAsync().ConfigureAwait(false);
            try
            {
                using var cmd = conn.CreateCommand();
                cmd.CommandText = @"
                    SELECT TOP 1 [Id], [Name], [SurName1], [SurName2]
                    FROM dbo.[Users]
                    WHERE [Id] = @id AND ([Deleted] = 0 OR [Deleted] IS NULL)";
                var p = cmd.CreateParameter();
                p.ParameterName = "@id";
                p.Value = id;
                cmd.Parameters.Add(p);

                using var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false);
                if (!await reader.ReadAsync().ConfigureAwait(false))
                    return null;
                return (
                    reader.IsDBNull(0) ? "" : reader.GetString(0),
                    reader.IsDBNull(1) ? null : reader.GetString(1),
                    reader.IsDBNull(2) ? null : reader.GetString(2),
                    reader.IsDBNull(3) ? null : reader.GetString(3));
            }
            finally
            {
                if (!wasOpen)
                    await conn.CloseAsync().ConfigureAwait(false);
            }
        }
    }
}
