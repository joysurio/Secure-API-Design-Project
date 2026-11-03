namespace LFS.SecureAPI.Services
{
    public interface IRefreshTokenStore
    {
        string Issue(string subject, TimeSpan ttl);
        bool TryGetSubject(string refreshToken, out string subject);
        void Revoke(string refreshToken);
        bool Rotate(string oldRefreshToken, TimeSpan ttl, out string newRefreshToken, out string subject);
    }
}
