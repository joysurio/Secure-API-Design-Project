using LFS.InfraStructure;
using LFS.SecureAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LFS.SecureAPI.Services
{
    public class NotificationService : INotificationService
    {
        private readonly DataContext _context;

        public NotificationService(DataContext context)
        {
            _context = context;
        }

        public async Task RegisterDeviceTokenAsync(string userId, NotificationRegisterRequest request)
        {
            await EnsureTableAsync();

            var platform = (request.Platform ?? "").Trim();
            var token = (request.Token ?? "").Trim();
            if (platform.Length == 0 || token.Length == 0)
                throw new ArgumentException("platform and token are required");

            await _context.Database.ExecuteSqlInterpolatedAsync($@"
MERGE DeviceTokens AS target
USING (SELECT {userId} AS UserId, {platform} AS Platform) AS source
ON (target.UserId = source.UserId AND target.Platform = source.Platform)
WHEN MATCHED THEN
    UPDATE SET Token = {token}, UpdatedAtUtc = GETUTCDATE()
WHEN NOT MATCHED THEN
    INSERT (UserId, Platform, Token, CreatedAtUtc, UpdatedAtUtc)
    VALUES ({userId}, {platform}, {token}, GETUTCDATE(), GETUTCDATE());");
        }

        private async Task EnsureTableAsync()
        {
            await _context.Database.ExecuteSqlRawAsync(@"
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeviceTokens]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[DeviceTokens] (
        [Id] INT IDENTITY(1,1) PRIMARY KEY,
        [UserId] NVARCHAR(128) NOT NULL,
        [Platform] NVARCHAR(20) NOT NULL,
        [Token] NVARCHAR(500) NOT NULL,
        [CreatedAtUtc] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        [UpdatedAtUtc] DATETIME2 NOT NULL DEFAULT GETUTCDATE()
    );
    CREATE UNIQUE INDEX UX_DeviceTokens_User_Platform ON [dbo].[DeviceTokens]([UserId], [Platform]);
END");
        }
    }
}
