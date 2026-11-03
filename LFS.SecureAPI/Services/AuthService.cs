using LFS.InfraStructure;
using LFS.Shared.Entities;
using LFS.SecureAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BCrypt.Net;

namespace LFS.SecureAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<DeviceRegistrationResponse> RegisterDeviceAsync(DeviceRegistrationRequest request)
        {
            // Check if device already exists
            var existingDevice = await _context.Devices.FirstOrDefaultAsync(d => d.DeviceId == request.DeviceId);
            if (existingDevice != null)
            {
                throw new InvalidOperationException($"Device {request.DeviceId} already exists");
            }

            // Hash the secret using BCrypt
            var secretHash = BCrypt.Net.BCrypt.HashPassword(request.SecretPlain);

            var device = new Device
            {
                DeviceId = request.DeviceId,
                SecretHash = secretHash,
                OwnerClientId = request.OwnerClientId,
                AllowedScopes = request.AllowedScopes,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            _context.Devices.Add(device);
            await _context.SaveChangesAsync();

            return new DeviceRegistrationResponse
            {
                DeviceId = device.DeviceId,
                Message = "Device registered successfully. Please store the secret securely and rotate it periodically."
            };
        }

        public async Task<TokenResponse> GetTokenAsync(TokenRequest request)
        {
            if (request.GrantType != "client_credentials")
            {
                throw new ArgumentException("Only client_credentials grant type is supported");
            }

            var device = await _context.Devices.FirstOrDefaultAsync(d => d.DeviceId == request.DeviceId);
            if (device == null || !device.IsActive)
            {
                throw new UnauthorizedAccessException("Invalid device credentials");
            }

            // Verify secret
            if (!BCrypt.Net.BCrypt.Verify(request.Secret, device.SecretHash))
            {
                throw new UnauthorizedAccessException("Invalid device credentials");
            }

            // Update LastSeen
            device.LastSeen = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            // Generate JWT token
            var token = GenerateJwtToken(device);

            return new TokenResponse
            {
                AccessToken = token,
                TokenType = "Bearer",
                ExpiresIn = _configuration.GetValue<int>("Jwt:ExpirationMinutes") * 60,
                Scope = device.AllowedScopes
            };
        }

        public async Task<bool> RevokeDeviceAsync(string deviceId)
        {
            var device = await _context.Devices.FirstOrDefaultAsync(d => d.DeviceId == deviceId);
            if (device == null)
            {
                return false;
            }

            device.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        private string GenerateJwtToken(Device device)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim("sub", device.DeviceId),
                new Claim("scope", device.AllowedScopes),
                new Claim("jti", Guid.NewGuid().ToString()),
                new Claim("iss", _configuration["Jwt:Issuer"]!),
                new Claim("aud", _configuration["Jwt:Audience"]!)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("Jwt:ExpirationMinutes")),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
