using System;

namespace LFS.Shared.Entities;

/// <summary>
/// Server-side refresh token for mobile / user sessions (replaces in-memory store).
/// Revoked on logout or rotation; removed when expired (optional cleanup).
/// </summary>
public class AuthRefreshToken
{
    public string Token { get; set; } = string.Empty;

    /// <summary>Refresh subject: AppUser id, <c>sc:{id}</c>, <c>qa:{id}</c>, etc.</summary>
    public string Subject { get; set; } = string.Empty;

    public DateTime ExpiresAtUtc { get; set; }

    public bool Revoked { get; set; }

    public DateTime CreatedAtUtc { get; set; }
}
