using LFS.InfraStructure;
using LFS.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace LFS.SecureAPI.Services;

/// <summary>
/// Persists refresh tokens in SQL Server so sessions survive API process restarts until logout or rotation invalidates the token.
/// </summary>
public sealed class EfRefreshTokenStore : IRefreshTokenStore
{
    private readonly DataContext _db;

    public EfRefreshTokenStore(DataContext db) => _db = db;

    public string Issue(string subject, TimeSpan ttl)
    {
        var token = Guid.NewGuid().ToString("N");
        var expires = JwtSessionLifetime.ClampSqlUtc(DateTime.UtcNow.Add(ttl));
        _db.AuthRefreshTokens.Add(new AuthRefreshToken
        {
            Token = token,
            Subject = subject,
            ExpiresAtUtc = expires,
            Revoked = false,
            CreatedAtUtc = DateTime.UtcNow
        });
        _db.SaveChanges();
        return token;
    }

    public bool TryGetSubject(string refreshToken, out string subject)
    {
        subject = string.Empty;
        if (string.IsNullOrEmpty(refreshToken))
            return false;

        var now = DateTime.UtcNow;
        var row = _db.AuthRefreshTokens.AsNoTracking()
            .FirstOrDefault(x => x.Token == refreshToken);
        if (row is null || row.Revoked || row.ExpiresAtUtc <= now)
            return false;

        subject = row.Subject;
        return true;
    }

    public void Revoke(string refreshToken)
    {
        if (string.IsNullOrEmpty(refreshToken))
            return;

        var row = _db.AuthRefreshTokens.FirstOrDefault(x => x.Token == refreshToken);
        if (row is null)
            return;

        row.Revoked = true;
        _db.SaveChanges();
    }

    public bool Rotate(string oldRefreshToken, TimeSpan ttl, out string newRefreshToken, out string subject)
    {
        newRefreshToken = string.Empty;
        subject = string.Empty;
        if (string.IsNullOrEmpty(oldRefreshToken))
            return false;

        string? capturedSubject = null;
        string? capturedNewTok = null;

        var ok = false;
        using (var tx = _db.Database.BeginTransaction())
        {
            try
            {
                var now = DateTime.UtcNow;
                var row = _db.AuthRefreshTokens.FirstOrDefault(x => x.Token == oldRefreshToken);
                if (row is null || row.Revoked || row.ExpiresAtUtc <= now)
                {
                    tx.Rollback();
                    return false;
                }

                row.Revoked = true;

                var newTok = Guid.NewGuid().ToString("N");
                var expires = JwtSessionLifetime.ClampSqlUtc(DateTime.UtcNow.Add(ttl));
                _db.AuthRefreshTokens.Add(new AuthRefreshToken
                {
                    Token = newTok,
                    Subject = row.Subject,
                    ExpiresAtUtc = expires,
                    Revoked = false,
                    CreatedAtUtc = DateTime.UtcNow
                });

                _db.SaveChanges();
                tx.Commit();

                capturedSubject = row.Subject;
                capturedNewTok = newTok;
                ok = true;
            }
            catch
            {
                tx.Rollback();
                ok = false;
            }
        }

        if (!ok || capturedSubject is null || capturedNewTok is null)
            return false;

        subject = capturedSubject;
        newRefreshToken = capturedNewTok;
        return true;
    }
}
