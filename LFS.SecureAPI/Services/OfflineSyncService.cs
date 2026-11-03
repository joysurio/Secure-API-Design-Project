using LFS.InfraStructure;
using LFS.Shared.Entities;
using LFS.SecureAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace LFS.SecureAPI.Services
{
    public class OfflineSyncService : IOfflineSyncService
    {
        private readonly DataContext _context;

        public OfflineSyncService(DataContext context)
        {
            _context = context;
        }

        public async Task<OfflineSyncResponse> SyncOfflineOperationsAsync(OfflineSyncRequest request)
        {
            // Check idempotency
            var existingOperation = await _context.OperationsLogs
                .FirstOrDefaultAsync(o => o.OperationId == request.OperationId);

            if (existingOperation != null)
            {
                return new OfflineSyncResponse
                {
                    OperationId = request.OperationId,
                    ProcessedAt = existingOperation.ProcessedAt,
                    ProcessedCount = 0,
                    Message = "Operation already processed"
                };
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                int processedCount = 0;

                foreach (var operation in request.Operations)
                {
                    switch (operation.Type.ToLower())
                    {
                        case "reposicion_add":
                            await ProcessReposicionAddAsync(operation);
                            processedCount++;
                            break;
                        case "inventory_count":
                            await ProcessInventoryCountAsync(operation);
                            processedCount++;
                            break;
                        case "scan":
                            await ProcessScanAsync(operation);
                            processedCount++;
                            break;
                        default:
                            // Unknown operation type - log but don't fail
                            break;
                    }
                }

                // Log the operation
                var operationLog = new OperationsLog
                {
                    OperationId = request.OperationId,
                    DeviceId = request.DeviceId,
                    UserId = request.UserId,
                    Endpoint = "/api/offline/sync",
                    PayloadHash = JsonSerializer.Serialize(request),
                    ProcessedAt = DateTime.UtcNow
                };

                _context.OperationsLogs.Add(operationLog);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return new OfflineSyncResponse
                {
                    OperationId = request.OperationId,
                    ProcessedAt = DateTime.UtcNow,
                    ProcessedCount = processedCount,
                    Message = "Sync completed successfully"
                };
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        private async Task ProcessReposicionAddAsync(OfflineOperation operation)
        {
            // Parse payload as MovReposicione
            var reposicion = JsonSerializer.Deserialize<MovReposicione>(operation.Payload ?? "{}");
            if (reposicion != null)
            {
                if (reposicion.IdRep == Guid.Empty)
                {
                    reposicion.IdRep = Guid.NewGuid();
                }
                if (reposicion.CreationDate == default)
                {
                    reposicion.CreationDate = DateTime.UtcNow;
                }

                _context.MovReposiciones.Add(reposicion);
                await _context.SaveChangesAsync();
            }
        }

        private async Task ProcessInventoryCountAsync(OfflineOperation operation)
        {
            // Process inventory count - implementation depends on your business logic
            // This is a placeholder
            await Task.CompletedTask;
        }

        private async Task ProcessScanAsync(OfflineOperation operation)
        {
            // Process scan operation - similar to RFID scan
            await Task.CompletedTask;
        }
    }
}
