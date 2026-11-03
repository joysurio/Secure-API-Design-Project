using LFS.InfraStructure;
using LFS.SecureAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LFS.SecureAPI.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly DataContext _context;

        public InventoryService(DataContext context)
        {
            _context = context;
        }

        public async Task<InventoryCountResponse> SubmitInventoryCountsAsync(InventoryCountRequest request)
        {
            // Check idempotency
            var existingOperation = await _context.OperationsLogs
                .FirstOrDefaultAsync(o => o.OperationId == request.OperationId);

            if (existingOperation != null)
            {
                return new InventoryCountResponse
                {
                    OperationId = request.OperationId,
                    ProcessedAt = existingOperation.ProcessedAt,
                    Message = "Operation already processed"
                };
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Process inventory counts
                // This is a simplified version - you may need to update specific inventory tables
                // based on your business logic

                var operationLog = new LFS.Shared.Entities.OperationsLog
                {
                    OperationId = request.OperationId,
                    DeviceId = request.DeviceId,
                    UserId = request.UserId,
                    Endpoint = "/api/inventory/counts",
                    PayloadHash = System.Text.Json.JsonSerializer.Serialize(request),
                    ProcessedAt = DateTime.UtcNow
                };

                _context.OperationsLogs.Add(operationLog);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return new InventoryCountResponse
                {
                    OperationId = request.OperationId,
                    ProcessedAt = DateTime.UtcNow,
                    Message = "Inventory counts submitted successfully"
                };
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<InventoryReconcileResponse> ReconcileInventoryAsync(int locationId)
        {
            var items = await _context.Items
                .Where(i => i.LocationId == locationId && !i.Deleted)
                .Select(i => new ItemDto
                {
                    Id = i.Id,
                    RFIDCode = i.RFIDCode,
                    Description = i.Description,
                    LocationId = i.LocationId,
                    LastMovementId = i.LastMovementId,
                    DateLastMov = i.DateLastMov
                })
                .ToListAsync();

            return new InventoryReconcileResponse
            {
                LocationId = locationId,
                ItemCount = items.Count,
                Items = items
            };
        }
    }
}
