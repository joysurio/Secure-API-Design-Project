using System.Collections.Concurrent;

namespace LFS.SecureAPI.Services
{
    public class InMemoryRefreshTokenStore : IRefreshTokenStore
    {
        private sealed class Entry
        {
            public string Subject { get; init; } = string.Empty;
            public DateTime ExpiresAtUtc { get; init; }
            public bool Revoked { get; set; }
        }

        private readonly ConcurrentDictionary<string, Entry> _tokens = new();

        public string Issue(string subject, TimeSpan ttl)
        {
            var token = Guid.NewGuid().ToString("N");
            _tokens[token] = new Entry
            {
                Subject = subject,
                ExpiresAtUtc = DateTime.UtcNow.Add(ttl),
                Revoked = false
            };
            return token;
        }

        public bool TryGetSubject(string refreshToken, out string subject)
        {
            subject = string.Empty;
            if (!_tokens.TryGetValue(refreshToken, out var entry))
                return false;

            if (entry.Revoked || entry.ExpiresAtUtc <= DateTime.UtcNow)
                return false;

            subject = entry.Subject;
            return true;
        }

        public void Revoke(string refreshToken)
        {
            if (_tokens.TryGetValue(refreshToken, out var entry))
                entry.Revoked = true;
        }

        public bool Rotate(string oldRefreshToken, TimeSpan ttl, out string newRefreshToken, out string subject)
        {
            newRefreshToken = string.Empty;
            subject = string.Empty;

            if (!TryGetSubject(oldRefreshToken, out subject))
                return false;

            Revoke(oldRefreshToken);
            newRefreshToken = Issue(subject, ttl);
            return true;
        }
    }
}
