using LFS.InfraStructure;
using LFS.Shared.Entities;
using LFS.SecureAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace LFS.SecureAPI.Services
{
    public class RfidService : IRfidService
    {
        private readonly DataContext _context;
        private readonly IAccessLogService _accessLogService;

        public RfidService(DataContext context, IAccessLogService accessLogService)
        {
            _context = context;
            _accessLogService = accessLogService;
        }

        public async Task<ItemDto?> GetItemByRfidAsync(string rfid)
        {
            var item = await _context.Items
                .Include(i => i.FamilyItemModel)
                .Include(i => i.ItemSize)
                .Include(i => i.Location)
                .FirstOrDefaultAsync(i => i.RFIDCode == rfid && !i.Deleted);

            if (item == null)
            {
                return null;
            }

            return new ItemDto
            {
                Id = item.Id,
                RFIDCode = item.RFIDCode,
                Description = item.Description,
                LocationId = item.LocationId,
                LastMovementId = item.LastMovementId,
                DateLastMov = item.DateLastMov,
                Personalised = item.Personalised,
                FamilyItemModelId = item.FamilyItemModelId,
                ItemSizeId = item.ItemSizeId
            };
        }

        public async Task<RfidScanResponse> ScanRfidTagsAsync(RfidScanRequest request)
        {
            // Check for duplicate operation (idempotency)
            var payloadHash = ComputeHash(JsonSerializer.Serialize(request));
            var existingOperation = await _context.OperationsLogs
                .FirstOrDefaultAsync(o => o.OperationId == request.OperationId);

            if (existingOperation != null)
            {
                // Return the same response (idempotent)
                return new RfidScanResponse
                {
                    OperationId = request.OperationId,
                    ProcessedAt = existingOperation.ProcessedAt,
                    Results = new List<RfidScanResult>() // Could store results in OperationsLog if needed
                };
            }

            var results = new List<RfidScanResult>();
            var deviceId = request.PdaId;

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                foreach (var tag in request.Tags)
                {
                    var result = await ProcessTagAsync(tag, request, deviceId);
                    results.Add(result);
                }

                // Log the operation
                var operationLog = new OperationsLog
                {
                    OperationId = request.OperationId,
                    DeviceId = deviceId,
                    UserId = request.UserId,
                    Endpoint = "/api/rfid/scan",
                    PayloadHash = payloadHash,
                    ProcessedAt = DateTime.UtcNow
                };

                _context.OperationsLogs.Add(operationLog);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return new RfidScanResponse
                {
                    OperationId = request.OperationId,
                    ProcessedAt = DateTime.UtcNow,
                    Results = results
                };
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        private async Task<RfidScanResult> ProcessTagAsync(RfidTagRequest tag, RfidScanRequest request, string? deviceId)
        {
            var item = await _context.Items
                .FirstOrDefaultAsync(i => i.RFIDCode == tag.Rfid && !i.Deleted);

            if (item == null)
            {
                if (request.CreateIfNotFound)
                {
                    item = new Item
                    {
                        RFIDCode = tag.Rfid,
                        CreationDate = DateTime.UtcNow,
                        LastDate = DateTime.UtcNow,
                        Deleted = false,
                        Personalised = false
                    };
                    _context.Items.Add(item);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    return new RfidScanResult
                    {
                        RFID = tag.Rfid,
                        Status = "not_found",
                        Item = null,
                        MovementId = null
                    };
                }
            }

            Movement? movement = null;
            if (request.RegisterMovement)
            {
                movement = new Movement
                {
                    Description = $"PDA scan from {deviceId}",
                    CreationDate = DateTime.UtcNow,
                    Deleted = false
                };
                _context.Movements.Add(movement);
                await _context.SaveChangesAsync();

                item.LastMovementId = movement.Id;
                item.DateLastMov = DateTime.UtcNow;
                _context.Items.Update(item);
                await _context.SaveChangesAsync();
            }

            return new RfidScanResult
            {
                RFID = tag.Rfid,
                Status = item.Id > 0 ? (item.CreationDate > DateTime.UtcNow.AddMinutes(-1) ? "created" : "found") : "not_found",
                Item = item != null ? new ItemDto
                {
                    Id = item.Id,
                    RFIDCode = item.RFIDCode,
                    Description = item.Description,
                    LocationId = item.LocationId,
                    LastMovementId = item.LastMovementId,
                    DateLastMov = item.DateLastMov,
                    Personalised = item.Personalised
                } : null,
                MovementId = movement?.Id
            };
        }

        private static string ComputeHash(string input)
        {
            using var sha256 = SHA256.Create();
            var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(hash);
        }
    }
}
