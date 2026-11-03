using LFS.InfraStructure;
using LFS.Shared.Entities;
using LFS.SecureAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace LFS.SecureAPI.Services
{
    public class LinenService : ILinenService
    {
        private readonly DataContext _context;
        private readonly IAccessLogService _accessLogService;

        public LinenService(DataContext context, IAccessLogService accessLogService)
        {
            _context = context;
            _accessLogService = accessLogService;
        }

        public async Task<TagRegistrationResponse> RegisterTagsAsync(TagRegistrationRequest request)
        {
            // Check for duplicate operation (idempotency)
            var existingOperation = await _context.OperationsLogs
                .FirstOrDefaultAsync(o => o.OperationId == request.OperationId);

            if (existingOperation != null)
            {
                // Return idempotent response
                return new TagRegistrationResponse
                {
                    OperationId = request.OperationId,
                    ProcessedAt = existingOperation.ProcessedAt,
                    Results = new List<TagRegistrationResult>()
                };
            }

            var results = new List<TagRegistrationResult>();

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                foreach (var tagItem in request.Tags)
                {
                    var result = await ProcessTagRegistrationAsync(tagItem, request.PdaId);
                    results.Add(result);
                }

                // Log the operation
                var operationLog = new OperationsLog
                {
                    OperationId = request.OperationId,
                    DeviceId = request.PdaId,
                    UserId = request.UserId,
                    Endpoint = "/api/linen/register-tags",
                    PayloadHash = ComputeHash(JsonSerializer.Serialize(request)),
                    ProcessedAt = DateTime.UtcNow
                };

                _context.OperationsLogs.Add(operationLog);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return new TagRegistrationResponse
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

        private async Task<TagRegistrationResult> ProcessTagRegistrationAsync(TagRegistrationItem tagItem, string deviceId)
        {
            try
            {
                var existingItem = await _context.Items
                    .FirstOrDefaultAsync(i => i.RFIDCode == tagItem.Rfid && !i.Deleted);

                if (existingItem != null)
                {
                    return new TagRegistrationResult
                    {
                        RFID = tagItem.Rfid,
                        Status = "already_exists",
                        ItemId = existingItem.Id
                    };
                }

                // Get FamilyItemTypeId from FamilyItemModel if FamilyItemModelId is provided
                int familyItemTypeId = 0;
                if (tagItem.FamilyItemModelId.HasValue)
                {
                    var familyItemModel = await _context.FamilyItemModels
                        .FirstOrDefaultAsync(m => m.Id == tagItem.FamilyItemModelId.Value);
                    if (familyItemModel != null && familyItemModel.FamilyItemTypeId.HasValue)
                    {
                        familyItemTypeId = familyItemModel.FamilyItemTypeId.Value;
                    }
                }

                var item = new Item
                {
                    RFIDCode = tagItem.Rfid,
                    Description = tagItem.Description ?? string.Empty,
                    CreationDate = DateTime.UtcNow,
                    LastDate = DateTime.UtcNow,
                    Deleted = false,
                    Personalised = false,
                    FamilyItemTypeId = familyItemTypeId,
                    FamilyItemModelId = tagItem.FamilyItemModelId ?? 0,
                    ItemSizeId = tagItem.ItemSizeId,
                    LocationId = tagItem.LocationId
                };

                _context.Items.Add(item);
                await _context.SaveChangesAsync();

                return new TagRegistrationResult
                {
                    RFID = tagItem.Rfid,
                    Status = "registered",
                    ItemId = item.Id
                };
            }
            catch (Exception ex)
            {
                return new TagRegistrationResult
                {
                    RFID = tagItem.Rfid,
                    Status = "error",
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<LinenDeliveryResponse> DeliverLinenAsync(LinenDeliveryRequest request)
        {
            // Check for duplicate operation (idempotency)
            var existingOperation = await _context.OperationsLogs
                .FirstOrDefaultAsync(o => o.OperationId == request.OperationId);

            if (existingOperation != null)
            {
                return new LinenDeliveryResponse
                {
                    OperationId = request.OperationId,
                    ProcessedAt = existingOperation.ProcessedAt,
                    Results = new List<LinenDeliveryResult>()
                };
            }

            var results = new List<LinenDeliveryResult>();

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                foreach (var deliveryItem in request.Items)
                {
                    var result = await ProcessDeliveryAsync(deliveryItem, request.PdaId, request.UserId);
                    results.Add(result);
                }

                // Log the operation
                var operationLog = new OperationsLog
                {
                    OperationId = request.OperationId,
                    DeviceId = request.PdaId,
                    UserId = request.UserId,
                    Endpoint = "/api/linen/deliver",
                    PayloadHash = ComputeHash(JsonSerializer.Serialize(request)),
                    ProcessedAt = DateTime.UtcNow
                };

                _context.OperationsLogs.Add(operationLog);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return new LinenDeliveryResponse
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

        private async Task<LinenDeliveryResult> ProcessDeliveryAsync(LinenDeliveryItem deliveryItem, string deviceId, int? userId)
        {
            try
            {
                var item = await _context.Items
                    .FirstOrDefaultAsync(i => i.RFIDCode == deliveryItem.Rfid && !i.Deleted);

                if (item == null)
                {
                    return new LinenDeliveryResult
                    {
                        RFID = deliveryItem.Rfid,
                        Status = "not_found"
                    };
                }

                // Create movement
                var movement = new Movement
                {
                    Description = $"Delivery - {deliveryItem.Remark ?? "No remark"}",
                    CreationDate = DateTime.UtcNow,
                    Deleted = false
                };
                _context.Movements.Add(movement);
                await _context.SaveChangesAsync();

                // Update item
                item.LastMovementId = movement.Id;
                item.DateLastMov = DateTime.UtcNow;
                _context.Items.Update(item);

                // Create linen operation record
                var linenOperation = new LinenOperation
                {
                    RFIDCode = deliveryItem.Rfid,
                    ItemId = item.Id,
                    OperationType = "Delivery",
                    Remark = deliveryItem.Remark,
                    DeviceId = deviceId,
                    UserId = userId,
                    OperationDate = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow,
                    Deleted = false
                };
                _context.LinenOperations.Add(linenOperation);
                await _context.SaveChangesAsync();

                return new LinenDeliveryResult
                {
                    RFID = deliveryItem.Rfid,
                    Status = "delivered",
                    ItemId = item.Id,
                    OperationId = linenOperation.Id
                };
            }
            catch (Exception ex)
            {
                return new LinenDeliveryResult
                {
                    RFID = deliveryItem.Rfid,
                    Status = "error",
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<AbnormalMarkingResponse> MarkAbnormalAsync(AbnormalMarkingRequest request)
        {
            // Check for duplicate operation (idempotency)
            var existingOperation = await _context.OperationsLogs
                .FirstOrDefaultAsync(o => o.OperationId == request.OperationId);

            if (existingOperation != null)
            {
                return new AbnormalMarkingResponse
                {
                    OperationId = request.OperationId,
                    ProcessedAt = existingOperation.ProcessedAt,
                    Results = new List<AbnormalResult>()
                };
            }

            var results = new List<AbnormalResult>();

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                foreach (var abnormalItem in request.Items)
                {
                    var result = await ProcessAbnormalMarkingAsync(abnormalItem, request.PdaId, request.UserId);
                    results.Add(result);
                }

                // Log the operation
                var operationLog = new OperationsLog
                {
                    OperationId = request.OperationId,
                    DeviceId = request.PdaId,
                    UserId = request.UserId,
                    Endpoint = "/api/linen/mark-abnormal",
                    PayloadHash = ComputeHash(JsonSerializer.Serialize(request)),
                    ProcessedAt = DateTime.UtcNow
                };

                _context.OperationsLogs.Add(operationLog);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return new AbnormalMarkingResponse
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

        private async Task<AbnormalResult> ProcessAbnormalMarkingAsync(AbnormalItem abnormalItem, string deviceId, int? userId)
        {
            try
            {
                var item = await _context.Items
                    .FirstOrDefaultAsync(i => i.RFIDCode == abnormalItem.Rfid && !i.Deleted);

                if (item == null)
                {
                    return new AbnormalResult
                    {
                        RFID = abnormalItem.Rfid,
                        Status = "not_found"
                    };
                }

                // Create movement
                var movement = new Movement
                {
                    Description = $"Abnormal - {abnormalItem.Remark}",
                    CreationDate = DateTime.UtcNow,
                    Deleted = false
                };
                _context.Movements.Add(movement);
                await _context.SaveChangesAsync();

                // Update item
                item.LastMovementId = movement.Id;
                item.DateLastMov = DateTime.UtcNow;
                _context.Items.Update(item);

                // Create linen operation record
                var linenOperation = new LinenOperation
                {
                    RFIDCode = abnormalItem.Rfid,
                    ItemId = item.Id,
                    OperationType = "Abnormal",
                    Remark = abnormalItem.Remark,
                    DeviceId = deviceId,
                    UserId = userId,
                    OperationDate = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow,
                    Deleted = false
                };
                _context.LinenOperations.Add(linenOperation);
                await _context.SaveChangesAsync();

                return new AbnormalResult
                {
                    RFID = abnormalItem.Rfid,
                    Status = "marked",
                    ItemId = item.Id,
                    OperationId = linenOperation.Id
                };
            }
            catch (Exception ex)
            {
                return new AbnormalResult
                {
                    RFID = abnormalItem.Rfid,
                    Status = "error",
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<WashedItemsImportResponse> ImportWashedItemsAsync(WashedItemsImportRequest request)
        {
            // Check for duplicate operation (idempotency)
            var existingOperation = await _context.OperationsLogs
                .FirstOrDefaultAsync(o => o.OperationId == request.OperationId);

            if (existingOperation != null)
            {
                return new WashedItemsImportResponse
                {
                    OperationId = request.OperationId,
                    ProcessedAt = existingOperation.ProcessedAt,
                    ReceivedDate = request.ReceivedDate ?? DateTime.UtcNow,
                    TotalItems = request.RfidList.Count,
                    FoundItems = 0,
                    NotFoundItems = 0,
                    Results = new List<WashedItemResult>()
                };
            }

            var results = new List<WashedItemResult>();
            var receivedDate = request.ReceivedDate ?? DateTime.UtcNow;
            int foundCount = 0;
            int notFoundCount = 0;

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                foreach (var rfid in request.RfidList)
                {
                    var result = await ProcessWashedItemAsync(rfid, request.PdaId, request.UserId, receivedDate);
                    results.Add(result);
                    if (result.Status == "received")
                        foundCount++;
                    else if (result.Status == "not_found")
                        notFoundCount++;
                }

                // Log the operation
                var operationLog = new OperationsLog
                {
                    OperationId = request.OperationId,
                    DeviceId = request.PdaId,
                    UserId = request.UserId,
                    Endpoint = "/api/linen/import-washed-items",
                    PayloadHash = ComputeHash(JsonSerializer.Serialize(request)),
                    ProcessedAt = DateTime.UtcNow
                };

                _context.OperationsLogs.Add(operationLog);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return new WashedItemsImportResponse
                {
                    OperationId = request.OperationId,
                    ProcessedAt = DateTime.UtcNow,
                    ReceivedDate = receivedDate,
                    TotalItems = request.RfidList.Count,
                    FoundItems = foundCount,
                    NotFoundItems = notFoundCount,
                    Results = results
                };
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        private async Task<WashedItemResult> ProcessWashedItemAsync(string rfid, string deviceId, int? userId, DateTime receivedDate)
        {
            try
            {
                var item = await _context.Items
                    .FirstOrDefaultAsync(i => i.RFIDCode == rfid && !i.Deleted);

                if (item == null)
                {
                    return new WashedItemResult
                    {
                        RFID = rfid,
                        Status = "not_found"
                    };
                }

                // Create movement
                var movement = new Movement
                {
                    Description = $"Washed items received from laundry company",
                    CreationDate = DateTime.UtcNow,
                    Deleted = false
                };
                _context.Movements.Add(movement);
                await _context.SaveChangesAsync();

                // Update item
                item.LastMovementId = movement.Id;
                item.DateLastMov = DateTime.UtcNow;
                _context.Items.Update(item);

                // Create linen operation record
                var linenOperation = new LinenOperation
                {
                    RFIDCode = rfid,
                    ItemId = item.Id,
                    OperationType = "WashedReceived",
                    Remark = $"Received from laundry company on {receivedDate:yyyy-MM-dd HH:mm:ss}",
                    DeviceId = deviceId,
                    UserId = userId,
                    OperationDate = receivedDate,
                    CreatedAt = DateTime.UtcNow,
                    Deleted = false
                };
                _context.LinenOperations.Add(linenOperation);
                await _context.SaveChangesAsync();

                return new WashedItemResult
                {
                    RFID = rfid,
                    Status = "received",
                    ItemId = item.Id,
                    OperationId = linenOperation.Id
                };
            }
            catch (Exception ex)
            {
                return new WashedItemResult
                {
                    RFID = rfid,
                    Status = "error",
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<RandomCheckResponse> RandomCheckAsync(RandomCheckRequest request)
        {
            // Check for duplicate operation (idempotency)
            var existingOperation = await _context.OperationsLogs
                .FirstOrDefaultAsync(o => o.OperationId == request.OperationId);

            if (existingOperation != null)
            {
                return new RandomCheckResponse
                {
                    OperationId = request.OperationId,
                    ProcessedAt = existingOperation.ProcessedAt,
                    Results = new List<RandomCheckResult>()
                };
            }

            var results = new List<RandomCheckResult>();
            var checkedDate = DateTime.UtcNow;

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                foreach (var rfid in request.RfidList)
                {
                    var result = await ProcessRandomCheckAsync(rfid, request.PdaId, request.UserId, checkedDate);
                    results.Add(result);
                }

                // Log the operation
                var operationLog = new OperationsLog
                {
                    OperationId = request.OperationId,
                    DeviceId = request.PdaId,
                    UserId = request.UserId,
                    Endpoint = "/api/linen/random-check",
                    PayloadHash = ComputeHash(JsonSerializer.Serialize(request)),
                    ProcessedAt = DateTime.UtcNow
                };

                _context.OperationsLogs.Add(operationLog);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return new RandomCheckResponse
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

        private async Task<RandomCheckResult> ProcessRandomCheckAsync(string rfid, string deviceId, int? userId, DateTime checkedDate)
        {
            try
            {
                var item = await _context.Items
                    .FirstOrDefaultAsync(i => i.RFIDCode == rfid && !i.Deleted);

                if (item == null)
                {
                    return new RandomCheckResult
                    {
                        RFID = rfid,
                        Status = "not_found",
                        CheckedDate = checkedDate
                    };
                }

                // Create movement
                var movement = new Movement
                {
                    Description = "Random check for washed linen",
                    CreationDate = DateTime.UtcNow,
                    Deleted = false
                };
                _context.Movements.Add(movement);
                await _context.SaveChangesAsync();

                // Update item
                item.LastMovementId = movement.Id;
                item.DateLastMov = DateTime.UtcNow;
                _context.Items.Update(item);

                // Create linen operation record
                var linenOperation = new LinenOperation
                {
                    RFIDCode = rfid,
                    ItemId = item.Id,
                    OperationType = "RandomCheck",
                    Remark = "Random check completed",
                    DeviceId = deviceId,
                    UserId = userId,
                    OperationDate = checkedDate,
                    CreatedAt = DateTime.UtcNow,
                    Deleted = false
                };
                _context.LinenOperations.Add(linenOperation);
                await _context.SaveChangesAsync();

                return new RandomCheckResult
                {
                    RFID = rfid,
                    Status = "checked",
                    ItemId = item.Id,
                    OperationId = linenOperation.Id,
                    CheckedDate = checkedDate
                };
            }
            catch (Exception ex)
            {
                return new RandomCheckResult
                {
                    RFID = rfid,
                    Status = "error",
                    CheckedDate = checkedDate,
                    ErrorMessage = ex.Message
                };
            }
        }

        private static string ComputeHash(string input)
        {
            using var sha256 = SHA256.Create();
            var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(hash);
        }
    }
}
