using LFS.InfraStructure;
using LFS.SecureAPI.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace LFS.SecureAPI.Services
{
    public class SupportTicketService : ISupportTicketService
    {
        private readonly DataContext _context;
        private readonly IMeService _meService;

        public SupportTicketService(DataContext context, IMeService meService)
        {
            _context = context;
            _meService = meService;
        }

        public async Task<string> CreateTicketAsync(string userId, SupportTicketRequest request)
        {
            await EnsureTableAsync();

            var ticketId = Guid.NewGuid().ToString();
            var subject = (request.Subject ?? "").Trim();
            var description = (request.Description ?? "").Trim();
            var imageUrl = (request.ImageUrl ?? "").Trim();
            var status = "open";

            await _context.Database.ExecuteSqlInterpolatedAsync($@"
INSERT INTO SupportTickets (TicketGuid, UserId, MachineId, Subject, Description, ImageUrl, Status, CreatedAtUtc)
VALUES ({ticketId}, {userId}, {request.MachineId}, {subject}, {description}, {imageUrl}, {status}, GETUTCDATE());");

            return ticketId;
        }

        public async Task<IReadOnlyList<SupportTicketAdminDto>> GetAdminTicketsAsync(string adminUserId, string? status)
        {
            await EnsureTableAsync();
            await RequireAdminAsync(adminUserId).ConfigureAwait(false);

            var normalizedStatus = NormalizeStatusFilter(status);

            var rawRows = new List<(string TicketGuid, string Status, string Subject, string Description, DateTime CreatedAtUtc, string UserId)>();
            var conn = _context.Database.GetDbConnection();
            var wasOpen = conn.State == ConnectionState.Open;
            if (!wasOpen)
                await conn.OpenAsync().ConfigureAwait(false);

            try
            {
                using var cmd = conn.CreateCommand();
                cmd.CommandText = @"
SELECT st.TicketGuid,
       st.[Status],
       st.Subject,
       st.[Description],
       st.CreatedAtUtc,
       st.UserId
FROM dbo.SupportTickets st
WHERE (@status IS NULL OR LOWER(st.[Status]) = @status)
ORDER BY st.CreatedAtUtc DESC";

                var statusParam = new SqlParameter("@status", SqlDbType.NVarChar, 30)
                {
                    Value = normalizedStatus is null ? DBNull.Value : normalizedStatus
                };
                cmd.Parameters.Add(statusParam);

                using var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false);
                while (await reader.ReadAsync().ConfigureAwait(false))
                {
                    rawRows.Add((
                        reader.IsDBNull(0) ? string.Empty : reader.GetString(0),
                        reader.IsDBNull(1) ? "open" : reader.GetString(1),
                        reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                        reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                        reader.IsDBNull(4) ? DateTime.UtcNow : reader.GetDateTime(4),
                        reader.IsDBNull(5) ? string.Empty : reader.GetString(5)
                    ));
                }
            }
            finally
            {
                if (!wasOpen)
                    await conn.CloseAsync().ConfigureAwait(false);
            }

            var userCache = new Dictionary<string, (string Name, string Role)>();
            var results = new List<SupportTicketAdminDto>();

            foreach (var row in rawRows)
            {
                if (!userCache.TryGetValue(row.UserId, out var userInfo))
                {
                    userInfo = await ResolveUserInfoAsync(row.UserId).ConfigureAwait(false);
                    userCache[row.UserId] = userInfo;
                }

                results.Add(new SupportTicketAdminDto
                {
                    TicketGuid = row.TicketGuid,
                    Status = row.Status,
                    Subject = row.Subject,
                    Description = row.Description,
                    CreatedAtUtc = row.CreatedAtUtc,
                    UserDisplayName = userInfo.Name,
                    UserRole = userInfo.Role
                });
            }

            return results;
        }

        private async Task<(string Name, string Role)> ResolveUserInfoAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return ("Unknown user", "User");

            try
            {
                var profile = await _meService.GetProfileAsync(userId).ConfigureAwait(false);
                var name = string.IsNullOrWhiteSpace(profile.Name) ? "Unknown user" : profile.Name;
                var role = string.IsNullOrWhiteSpace(profile.Role) ? "User" : profile.Role;
                return (name, role);
            }
            catch
            {
                return await ResolveUserFromSubClienteTableAsync(userId).ConfigureAwait(false);
            }
        }

        private async Task<(string Name, string Role)> ResolveUserFromSubClienteTableAsync(string userId)
        {
            int subClienteId = 0;
            var raw = userId.Trim();
            if (raw.StartsWith("sc:", StringComparison.OrdinalIgnoreCase))
                int.TryParse(raw[3..], out subClienteId);
            else if (raw.StartsWith("sc", StringComparison.OrdinalIgnoreCase))
                int.TryParse(raw[2..], out subClienteId);

            if (subClienteId <= 0)
                return ("User #" + raw, "User");

            try
            {
                var sc = await _context.MasterSubClientes
                    .AsNoTracking()
                    .Where(s => s.Id == subClienteId)
                    .Select(s => new { s.Nombre, s.Apellido1, s.Apellido2, s.Puesto, s.Categoria })
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);

                if (sc is null)
                    return ("User #" + subClienteId, "User");

                var name = string.Join(" ",
                    new[] { sc.Nombre, sc.Apellido1, sc.Apellido2 }
                        .Where(s => !string.IsNullOrWhiteSpace(s)));
                var role = !string.IsNullOrWhiteSpace(sc.Puesto) ? sc.Puesto
                         : !string.IsNullOrWhiteSpace(sc.Categoria) ? sc.Categoria
                         : "User";

                return (
                    string.IsNullOrWhiteSpace(name) ? "User #" + subClienteId : name.Trim(),
                    role.Trim()
                );
            }
            catch
            {
                return ("User #" + subClienteId, "User");
            }
        }

        public async Task ResolveTicketAsync(string adminUserId, string ticketGuid)
        {
            await EnsureTableAsync();
            await RequireAdminAsync(adminUserId).ConfigureAwait(false);

            var normalizedGuid = (ticketGuid ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(normalizedGuid))
                throw new InvalidOperationException("Ticket id is required.");

            var rows = await _context.Database.ExecuteSqlInterpolatedAsync($@"
UPDATE dbo.SupportTickets
SET [Status] = 'resolved',
    [HandledBy] = {adminUserId}
WHERE [TicketGuid] = {normalizedGuid}
  AND LOWER([Status]) <> 'resolved';").ConfigureAwait(false);

            if (rows == 0)
                throw new InvalidOperationException("Ticket was not found or is already resolved.");
        }

        private async Task EnsureTableAsync()
        {
            await _context.Database.ExecuteSqlRawAsync(@"
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SupportTickets]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[SupportTickets] (
        [Id] INT IDENTITY(1,1) PRIMARY KEY,
        [TicketGuid] NVARCHAR(64) NOT NULL,
        [UserId] NVARCHAR(128) NOT NULL,
        [MachineId] INT NULL,
        [Subject] NVARCHAR(200) NOT NULL,
        [Description] NVARCHAR(2000) NOT NULL,
        [ImageUrl] NVARCHAR(1000) NULL,
        [Status] NVARCHAR(50) NOT NULL,
        [CreatedAtUtc] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        [HandledBy] NVARCHAR(128) NULL
    );
    CREATE UNIQUE INDEX UX_SupportTickets_TicketGuid ON [dbo].[SupportTickets]([TicketGuid]);
END");
        }

        private async Task RequireAdminAsync(string userId)
        {
            var me = await _meService.GetProfileAsync(userId).ConfigureAwait(false);
            if (!string.Equals(me.Role, "admin", StringComparison.OrdinalIgnoreCase))
                throw new UnauthorizedAccessException("Administrator role required.");
        }

        private static string? NormalizeStatusFilter(string? status)
        {
            if (string.IsNullOrWhiteSpace(status))
                return null;

            return status.Trim().ToLowerInvariant() switch
            {
                "all" => null,
                "pending" => "open",
                "open" => "open",
                "resolved" => "resolved",
                _ => null
            };
        }
    }
}
