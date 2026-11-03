using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LFS.SecureAPI.Models
{
    /// <summary>
    /// Login request for SubCliente (employee) users.
    /// Username can be MasterSubClientes Id, DNI, or IDEmpleado.
    /// Password is validated against Campo2 from Mastersubclientes.
    /// </summary>
    public class SubClienteLoginRequest
    {
        [Required]
        [JsonPropertyName("username")]
        public string Username { get; set; } = string.Empty;

        [Required]
        [JsonPropertyName("password")]
        public string Password { get; set; } = string.Empty;
    }
}
