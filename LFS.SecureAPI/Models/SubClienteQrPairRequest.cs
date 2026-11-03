using System.Text.Json.Serialization;

namespace LFS.SecureAPI.Models
{
    /// <summary>
    /// Mobile QR onboarding: pair device to a SubCliente without password (hospital-issued QR).
    /// </summary>
    public class SubClienteQrPairRequest
    {
        /// <summary>
        /// MasterSubClientes primary key, DNI, IDEmpleado, or <see cref="MasterSubClientes.CodigoQr"/> (trimmed, ordinal).
        /// </summary>
        [JsonPropertyName("userId")]
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// Optional; must match <see cref="MobileQrPairingOptions.SharedSecret"/> when that option is set (alternative to header).
        /// </summary>
        [JsonPropertyName("pairingSecret")]
        public string? PairingSecret { get; set; }

        /// <summary>
        /// When <c>admin</c> (case-insensitive), <see cref="UserId"/> must be <c>Users.Id</c> (text AppUser identifier).
        /// When omitted or any other value, resolution uses only <see cref="LFS.Shared.Entities.MasterSubClientes"/>.
        /// </summary>
        [JsonPropertyName("role")]
        public string? Role { get; set; }
    }
}
