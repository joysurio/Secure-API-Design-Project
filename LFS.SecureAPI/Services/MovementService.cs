using LFS.InfraStructure;
using LFS.Shared.Entities;
using LFS.SecureAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LFS.SecureAPI.Services
{
    public class MovementService : IMovementService
    {
        private readonly DataContext _context;

        public MovementService(DataContext context)
        {
            _context = context;
        }

        public async Task<MovementResponse> CreateMovementAsync(CreateMovementRequest request)
        {
            var movement = new Movement
            {
                Description = request.Description,
                CreationDate = DateTime.UtcNow,
                Deleted = false
            };

            _context.Movements.Add(movement);
            await _context.SaveChangesAsync();

            return new MovementResponse
            {
                Id = movement.Id,
                Description = movement.Description,
                CreationDate = movement.CreationDate
            };
        }

        public async Task<MovementResponse> CreateItemMovementAsync(int itemId, CreateItemMovementRequest request)
        {
            var item = await _context.Items.FindAsync(itemId);
            if (item == null || item.Deleted)
            {
                throw new KeyNotFoundException($"Item with ID {itemId} not found");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var movement = new Movement
                {
                    Description = request.Description ?? $"Movement for item {itemId}",
                    CreationDate = DateTime.UtcNow,
                    Deleted = false
                };

                _context.Movements.Add(movement);
                await _context.SaveChangesAsync();

                item.LastMovementId = movement.Id;
                item.DateLastMov = DateTime.UtcNow;
                if (request.LocationId.HasValue)
                {
                    item.LocationId = request.LocationId.Value;
                }

                _context.Items.Update(item);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return new MovementResponse
                {
                    Id = movement.Id,
                    Description = movement.Description,
                    CreationDate = movement.CreationDate
                };
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
