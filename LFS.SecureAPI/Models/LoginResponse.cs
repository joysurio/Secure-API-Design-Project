using System.Text.Json.Serialization;

namespace LFS.SecureAPI.Models
{
    /// <summary>
    /// User login response with JWT tokens per CTO MVP spec.
    /// </summary>
    public class LoginResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; } = string.Empty;

        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; } = string.Empty;

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }

        /// <summary>Application role: <c>admin</c> or <c>user</c> (SubCliente: ApplicationUsers table; AppUser: Identity + ApplicationUsers).</summary>
        [JsonPropertyName("role")]
        public string Role { get; set; } = "user";
    }
}
