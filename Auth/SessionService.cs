using InfluencersPlatformBackend.Data;
using InfluencersPlatformBackend.Helpers;
using InfluencersPlatformBackend.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace InfluencersPlatformBackend.Auth
{
    public class SessionService(ApplicationDBContext dBContext)
    {
        public async Task CreateSessionAsync (Guid sessionId, string userId, string refreshToken, DateTime expiresAt)
        {
            dBContext.Sessions.Add(new Session
            {
                Id = sessionId,
                UserId = userId,
                InitiatedAt = DateTimeOffset.UtcNow,
                ExpiresAt = expiresAt,
                LastRefreshToken = refreshToken.ToSHA256()
            });
            await dBContext.SaveChangesAsync();
        }

        public async Task ExtendSessionAsync (Guid sessionId, string refreshToken, DateTime expiresAt)
        {
            var session = await dBContext.Sessions.FindAsync(sessionId);
            session.ExpiresAt = expiresAt;
            session.LastRefreshToken = refreshToken.ToSHA256();

            await dBContext.SaveChangesAsync();
        }

        public async Task InvalidateSessionAsync (Guid sessionId)
        {
            var session = await dBContext.Sessions.FindAsync (sessionId);
            if (session is null)
            {
                return;
            }
            session.IsRevoked = true;
            await dBContext.SaveChangesAsync();
        }

        public async Task<bool> IsSessionValidAsync (Guid sessionId, string refreshToken)
        {
            var session = await dBContext.Sessions.FindAsync(sessionId);
            return session is not null && session.ExpiresAt > DateTimeOffset.UtcNow && !session.IsRevoked &&
                session.LastRefreshToken == refreshToken.ToSHA256();  
        }
    }
}
