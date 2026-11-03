namespace LFS.SecureAPI.Models;

/// <summary>
/// Settings for mobile app QR device pairing (<c>POST /api/Auth/subcliente/qr-pair</c>).
/// </summary>
public class MobileQrPairingOptions
{
    public const string SectionName = "MobileQrPairing";

    /// <summary>When false, the QR-pair endpoint returns 404 (feature off).</summary>
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// Optional shared secret. When non-empty, requests must send the same value in
    /// header <c>X-LFS-Mobile-Qr-Secret</c> or in JSON body <c>pairingSecret</c>.
    /// </summary>
    public string? SharedSecret { get; set; }
}
