using LFS.InfraStructure;
using LFS.Shared.Entities;
using LFS.SecureAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace LFS.SecureAPI.Services
{
    /// <summary>
    /// Atomic transaction: insert movement + update credits + update machine stock.
    /// Uses single DB transaction. Machine stock is via PrendasDisponiblesEnMaquinaDispensacion (view);
    /// underlying table updates depend on schema - extend as needed.
    /// </summary>
    public class TransactionService : ITransactionService
    {
        private readonly DataContext _context;

        public TransactionService(DataContext context)
        {
            _context = context;
        }

        public async Task<TransactionResponse> CreateTransactionAsync(string userId, TransactionRequest request)
        {
            if (!int.TryParse(request.MachineId, out var machineId))
                throw new ArgumentException("Invalid machine_id");
            if (request.Items == null || request.Items.Count == 0)
                throw new ArgumentException("At least one item is required");

            var txId = Guid.NewGuid();
            var now = DateTime.UtcNow;
            var isSubCliente = TryParseSubClienteId(userId, out var subClienteId);

            using var dbTransaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);

            try
            {
                foreach (var item in request.Items)
                {
                    if (!int.TryParse(item.ModelId, out var modelId))
                        throw new ArgumentException($"Invalid model_id: {item.ModelId}");
                    if (item.Qty <= 0)
                        throw new ArgumentException("qty must be greater than zero");

                    var normalizedSize = (item.Size ?? string.Empty).Trim();
                    if (normalizedSize.Length == 0)
                        throw new ArgumentException("size is required");

                    await ApplyMachineStockAsync(machineId, modelId, normalizedSize, item.Qty, request.Type);

                    if (isSubCliente)
                        await ApplySubClienteCreditsAsync(subClienteId, modelId, normalizedSize, item.Qty, request.Type);

                    var movement = new Movement
                    {
                        Description = BuildDescription(
                            request.Type, machineId, modelId, normalizedSize, item.Qty, userId, txId, "complete"),
                        Deleted = false,
                        CreationDate = now
                    };
                    _context.Movements.Add(movement);
                }

                await _context.SaveChangesAsync();
                await dbTransaction.CommitAsync();

                return new TransactionResponse
                {
                    Id = txId.ToString(),
                    Status = "complete"
                };
            }
            catch
            {
                await dbTransaction.RollbackAsync();
                throw;
            }
        }

        public async Task<TransactionResponse> CancelTransactionAsync(string userId, string transactionId)
        {
            if (!Guid.TryParse(transactionId, out _))
                throw new ArgumentException("Invalid transaction id");

            var rows = await _context.Movements
                .Where(m => !m.Deleted && m.Description.Contains($"txId={transactionId}"))
                .ToListAsync();

            if (rows.Count == 0)
                throw new InvalidOperationException("Transaction not found");

            using var dbTransaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                foreach (var row in rows)
                {
                    var data = ParseDescription(row.Description);
                    if (!data.TryGetValue("status", out var status) || !string.Equals(status, "complete", StringComparison.OrdinalIgnoreCase))
                        throw new InvalidOperationException("Transaction already cancelled or not reversible");

                    var type = data["type"];
                    var reverseType = string.Equals(type, "collect", StringComparison.OrdinalIgnoreCase) ? "return" : "collect";
                    var machineId = int.Parse(data["machine"], CultureInfo.InvariantCulture);
                    var modelId = int.Parse(data["model"], CultureInfo.InvariantCulture);
                    var qty = int.Parse(data["qty"], CultureInfo.InvariantCulture);
                    var size = data["size"];

                    await ApplyMachineStockAsync(machineId, modelId, size, qty, reverseType);

                    if (TryParseSubClienteId(userId, out var subClienteId))
                        await ApplySubClienteCreditsAsync(subClienteId, modelId, size, qty, reverseType);

                    row.Deleted = true;
                    var cancelMovement = new Movement
                    {
                        Description = BuildDescription(
                            "cancel", machineId, modelId, size, qty, userId, Guid.Parse(transactionId), "complete"),
                        Deleted = false,
                        CreationDate = DateTime.UtcNow
                    };
                    _context.Movements.Add(cancelMovement);
                }

                await _context.SaveChangesAsync();
                await dbTransaction.CommitAsync();

                return new TransactionResponse { Id = transactionId, Status = "cancelled" };
            }
            catch
            {
                await dbTransaction.RollbackAsync();
                throw;
            }
        }

        private async Task ApplyMachineStockAsync(int machineId, int modelId, string size, int qty, string type)
        {
            if (string.Equals(type, "collect", StringComparison.OrdinalIgnoreCase))
            {
                var affected = await _context.Database.ExecuteSqlInterpolatedAsync($@"
UPDATE MovMaquinaDisAlmacenes
SET StockReal = StockReal - {qty}
WHERE IdMaquinaDispensacion = {machineId}
  AND IdModelo = {modelId}
  AND ISNULL(Talla, '') = {size}
  AND StockReal >= {qty};");
                if (affected == 0)
                    throw new InvalidOperationException("Not enough machine stock");
                return;
            }

            if (string.Equals(type, "return", StringComparison.OrdinalIgnoreCase))
            {
                var affected = await _context.Database.ExecuteSqlInterpolatedAsync($@"
UPDATE MovMaquinaDisAlmacenes
SET StockReal = StockReal + {qty}
WHERE IdMaquinaDispensacion = {machineId}
  AND IdModelo = {modelId}
  AND ISNULL(Talla, '') = {size};");
                if (affected == 0)
                    throw new InvalidOperationException("Machine stock row not found");
                return;
            }

            throw new ArgumentException("type must be collect or return");
        }

        private async Task ApplySubClienteCreditsAsync(int subClienteId, int modelId, string size, int qty, string type)
        {
            if (string.Equals(type, "collect", StringComparison.OrdinalIgnoreCase))
            {
                var affected = await _context.Database.ExecuteSqlInterpolatedAsync($@"
UPDATE MovStockPrendasUsu
SET Cantidad = Cantidad + {qty}
WHERE IdSubCliente = {subClienteId}
  AND IdModelo = {modelId}
  AND ISNULL(Talla, '') = {size};");

                if (affected == 0)
                {
                    await _context.Database.ExecuteSqlInterpolatedAsync($@"
INSERT INTO MovStockPrendasUsu (IdSubCliente, IdModelo, Talla, Cantidad)
VALUES ({subClienteId}, {modelId}, {size}, {qty});");
                }
                return;
            }

            if (string.Equals(type, "return", StringComparison.OrdinalIgnoreCase))
            {
                var affected = await _context.Database.ExecuteSqlInterpolatedAsync($@"
UPDATE MovStockPrendasUsu
SET Cantidad = Cantidad - {qty}
WHERE IdSubCliente = {subClienteId}
  AND IdModelo = {modelId}
  AND ISNULL(Talla, '') = {size}
  AND Cantidad >= {qty};");
                if (affected == 0)
                    throw new InvalidOperationException("Not enough assigned garments for return");
                return;
            }
        }

        private static string BuildDescription(
            string type, int machineId, int modelId, string size, int qty, string userId, Guid txId, string status)
        {
            return $"{type} | machine={machineId} | model={modelId} | size={size} | qty={qty} | userId={userId} | txId={txId} | status={status}";
        }

        private static Dictionary<string, string> ParseDescription(string description)
        {
            var map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            var parts = description.Split('|', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length > 0)
                map["type"] = parts[0];

            foreach (var part in parts.Skip(1))
            {
                var idx = part.IndexOf('=');
                if (idx <= 0 || idx == part.Length - 1)
                    continue;
                var key = part[..idx].Trim();
                var value = part[(idx + 1)..].Trim();
                map[key] = value;
            }
            return map;
        }

        private static bool TryParseSubClienteId(string userId, out int subClienteId)
        {
            subClienteId = 0;
            return userId.StartsWith("sc:", StringComparison.Ordinal)
                && int.TryParse(userId[3..], out subClienteId);
        }
    }
}
