using System;
using Microsoft.Extensions.Configuration;

namespace LFS.SecureAPI.Services;

/// <summary>
/// JWT + refresh lifetimes for mobile / interactive logins. When <c>PersistentSessionUntilLogout</c>
/// is true, access and refresh tokens last until explicit logout (revocation), capped at a SQL-safe max date.
/// </summary>
internal static class JwtSessionLifetime
{
    /// <summary>SQL Server <c>datetime2</c> practical upper bound for expiry columns and JWT <c>exp</c>.</summary>
    private static readonly DateTime SqlMaxUtc = new(9999, 12, 31, 23, 59, 59, DateTimeKind.Utc);

    public static bool PersistentSessionEnabled(IConfiguration configuration) =>
        configuration.GetValue("Jwt:PersistentSessionUntilLogout", true);

    public static TimeSpan GetRefreshTokenTtl(IConfiguration configuration)
    {
        if (PersistentSessionEnabled(configuration))
            return TimeSpan.FromDays(365 * 50);

        return TimeSpan.FromDays(configuration.GetValue("Jwt:RefreshTokenDays", 14));
    }

    public static TimeSpan GetMobileAccessTokenLifetime(IConfiguration configuration)
    {
        if (PersistentSessionEnabled(configuration))
            return TimeSpan.FromDays(365 * 50);

        return TimeSpan.FromMinutes(configuration.GetValue("Jwt:ExpirationMinutes", 60));
    }

    public static int GetMobileExpiresInSeconds(IConfiguration configuration)
    {
        var seconds = (long)GetMobileAccessTokenLifetime(configuration).TotalSeconds;
        return seconds > int.MaxValue ? int.MaxValue : (int)seconds;
    }

    public static DateTime GetMobileAccessTokenExpiresUtc(IConfiguration configuration) =>
        ClampSqlUtc(DateTime.UtcNow.Add(GetMobileAccessTokenLifetime(configuration)));

    public static DateTime ClampSqlUtc(DateTime utc)
    {
        if (utc.Kind != DateTimeKind.Utc)
            utc = utc.ToUniversalTime();
        return utc > SqlMaxUtc ? SqlMaxUtc : utc;
    }
}
