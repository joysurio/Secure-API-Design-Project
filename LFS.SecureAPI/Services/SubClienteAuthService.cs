using System.Globalization;
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
    /// <summary>
    /// Authenticates SubCliente (employee) users by matching Username (MasterSubClientes Id, DNI, or IDEmpleado)
    /// and Password (Campo2) against the Mastersubclientes table, then issues a JWT.
    /// Admin QR pairing: request body must set role to admin; userId is resolved in Users (AppUser.Id text).
    /// Otherwise userId is resolved only in MasterSubClientes (employee QR).
    /// </summary>
    public class SubClienteAuthService : ISubClienteAuthService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IRefreshTokenStore _refreshTokenStore;
        private readonly IApplicationUsersAdminService _applicationUsersAdmin;
        private readonly UserManager<AppUser> _userManager;

        public SubClienteAuthService(
            DataContext context,
            IConfiguration configuration,
            IRefreshTokenStore refreshTokenStore,
            IApplicationUsersAdminService applicationUsersAdmin,
            UserManager<AppUser> userManager)
        {
            _context = context;
            _configuration = configuration;
            _refreshTokenStore = refreshTokenStore;
            _applicationUsersAdmin = applicationUsersAdmin;
            _userManager = userManager;
        }

        public async Task<LoginResponse> LoginAsync(SubClienteLoginRequest request)
        {
            var username = request.Username.Trim();
            var password = request.Password;

            SubClienteAuthSlice? slice = null;

            if (int.TryParse(username, NumberStyles.Integer, CultureInfo.InvariantCulture, out var numericId))
            {
                slice = await _context.MasterSubClientes
                    .AsNoTracking()
                    .Where(s => s.Id == numericId && s.Campo2 == password && s.Borrado != true)
                    .Select(s => new SubClienteAuthSlice(s.Id, s.Nombre, s.Apellido1, s.Dni, s.IdEmpleado))
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);
            }

            if (slice == null)
            {
                slice = await _context.MasterSubClientes
                    .AsNoTracking()
                    .Where(s =>
                        (s.Dni == username || s.IdEmpleado == username) &&
                        s.Campo2 == password &&
                        s.Borrado != true)
                    .Select(s => new SubClienteAuthSlice(s.Id, s.Nombre, s.Apellido1, s.Dni, s.IdEmpleado))
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);
            }

            if (slice == null)
                throw new UnauthorizedAccessException("Invalid username or password");

            return await BuildLoginResponseForSubClienteAsync(slice).ConfigureAwait(false);
        }

        /// <summary>
        /// QR-based pairing: see <see cref="SubClienteQrPairRequest.Role"/> — admin QR → Users(AppUser.Id) only; otherwise MasterSubClientes only.
        /// </summary>
        public async Task<LoginResponse> QrPairAsync(SubClienteQrPairRequest request)
        {
            var key = (request.UserId ?? string.Empty).Trim();
            if (string.IsNullOrEmpty(key))
                throw new UnauthorizedAccessException("userId is required");

            if (IsAdminQrRole(request.Role))
            {
                var adminUser = await _userManager.Users
                    .FirstOrDefaultAsync(u => u.Id == key && !u.Deleted)
                    .ConfigureAwait(false);
                if (adminUser != null)
                {
                    var roles = await _userManager.GetRolesAsync(adminUser).ConfigureAwait(false);
                    return BuildLoginResponseForQrAdmin(adminUser.Id, adminUser.Name, adminUser.SurName1, adminUser.SurName2, adminUser.Email, roles);
                }

                var usersRow = await FindUsersTableRowByIdAsync(key, default).ConfigureAwait(false);
                if (usersRow != null)
                    return BuildLoginResponseForQrAdmin(usersRow.Value.Id, usersRow.Value.Name, usersRow.Value.SurName1, usersRow.Value.SurName2, null, Array.Empty<string>());

                throw new UnauthorizedAccessException("User not found or inactive");
            }

            SubClienteAuthSlice? slice = await FindSubClienteByKeyAsync(key).ConfigureAwait(false);

            if (slice != null)
                return await BuildLoginResponseForSubClienteAsync(slice).ConfigureAwait(false);

            throw new UnauthorizedAccessException("User not found or inactive");
        }

        /// <summary>
        /// Resolves an active SubCliente by primary key, DNI, IDEmpleado, or CodigoQr (same order as QR pairing).
        /// </summary>
        private async Task<SubClienteAuthSlice?> FindSubClienteByKeyAsync(string key)
        {
            if (int.TryParse(key, NumberStyles.Integer, CultureInfo.InvariantCulture, out var numericId))
            {
                var byId = await _context.MasterSubClientes
                    .AsNoTracking()
                    .Where(s => s.Id == numericId && s.Borrado != true)
                    .Select(s => new SubClienteAuthSlice(s.Id, s.Nombre, s.Apellido1, s.Dni, s.IdEmpleado))
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);
                if (byId != null)
                    return byId;
            }

            var byDniOrEmployee = await _context.MasterSubClientes
                .AsNoTracking()
                .Where(s => (s.Dni == key || s.IdEmpleado == key) && s.Borrado != true)
                .Select(s => new SubClienteAuthSlice(s.Id, s.Nombre, s.Apellido1, s.Dni, s.IdEmpleado))
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
            if (byDniOrEmployee != null)
                return byDniOrEmployee;

            if (string.IsNullOrEmpty(key))
                return null;

            return await _context.MasterSubClientes
                .AsNoTracking()
                .Where(s =>
                    s.Borrado != true
                    && !string.IsNullOrWhiteSpace(s.CodigoQr)
                    && string.Equals(s.CodigoQr.Trim(), key, StringComparison.Ordinal))
                .Select(s => new SubClienteAuthSlice(s.Id, s.Nombre, s.Apellido1, s.Dni, s.IdEmpleado))
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
        }

        private static bool IsAdminQrRole(string? role) =>
            !string.IsNullOrWhiteSpace(role)
            && string.Equals(role.Trim(), "admin", StringComparison.OrdinalIgnoreCase);

        private async Task<LoginResponse> BuildLoginResponseForSubClienteAsync(SubClienteAuthSlice subCliente)
        {
            var idEmp = ApplicationUsersAdminService.ParseEmployeeId(subCliente.IdEmpleado);
            var isAdmin = await _applicationUsersAdmin
                .IsListedInApplicationUsersAsync(subCliente.Dni, idEmp)
                .ConfigureAwait(false);

            var token = GenerateJwt(subCliente.Id, subCliente.Nombre, subCliente.Apellido1, isAdmin);

            var refreshTtl = JwtSessionLifetime.GetRefreshTokenTtl(_configuration);
            return new LoginResponse
            {
                AccessToken = token,
                RefreshToken = _refreshTokenStore.Issue($"sc:{subCliente.Id}", refreshTtl),
                ExpiresIn = JwtSessionLifetime.GetMobileExpiresInSeconds(_configuration),
                Role = isAdmin ? "admin" : "user"
            };
        }

        private LoginResponse BuildLoginResponseForQrAdmin(
            string userId,
            string? name,
            string? surName1,
            string? surName2,
            string? email,
            IList<string> roles)
        {
            var fullName = string.Join(" ",
                new[] { name, surName1, surName2 }
                    .Where(s => !string.IsNullOrWhiteSpace(s)));

            var accessToken = GenerateIdentityUserJwt(userId, email, fullName, roles);
            var refreshTtl = JwtSessionLifetime.GetRefreshTokenTtl(_configuration);
            return new LoginResponse
            {
                AccessToken = accessToken,
                RefreshToken = _refreshTokenStore.Issue($"qa:{userId}", refreshTtl),
                ExpiresIn = JwtSessionLifetime.GetMobileExpiresInSeconds(_configuration),
                Role = "admin"
            };
        }

        private string GenerateIdentityUserJwt(string userId, string? email, string fullName, IList<string> roles)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim("sub", $"qa:{userId}"),
                new Claim("auth_type", "admin_user"),
                new Claim("email", email ?? ""),
                new Claim("name", fullName),
                new Claim("jti", Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, "admin")
            };
            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: JwtSessionLifetime.GetMobileAccessTokenExpiresUtc(_configuration),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<(string Id, string? Name, string? SurName1, string? SurName2)?> FindUsersTableRowByIdAsync(string id, CancellationToken ct)
        {
            var conn = _context.Database.GetDbConnection();
            var wasOpen = conn.State == System.Data.ConnectionState.Open;
            if (!wasOpen)
                await conn.OpenAsync(ct).ConfigureAwait(false);
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

                using var reader = await cmd.ExecuteReaderAsync(ct).ConfigureAwait(false);
                if (!await reader.ReadAsync(ct).ConfigureAwait(false))
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

        private string GenerateJwt(int subClienteId, string? nombre, string? apellido, bool isApplicationAdministrator)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var fullName = string.Join(" ", new[] { nombre, apellido }
                .Where(s => !string.IsNullOrWhiteSpace(s)));

            var claims = new List<Claim>
            {
                new Claim("sub", $"sc:{subClienteId}"),
                new Claim("auth_type", "user"),
                new Claim("subcliente_id", subClienteId.ToString()),
                new Claim("name", fullName),
                new Claim("jti", Guid.NewGuid().ToString())
            };

            if (isApplicationAdministrator)
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
