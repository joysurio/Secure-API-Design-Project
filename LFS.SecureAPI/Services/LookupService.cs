using LFS.InfraStructure;
using LFS.Shared.Entities;
using LFS.SecureAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LFS.SecureAPI.Services
{
    public class LookupService : ILookupService
    {
        private readonly DataContext _context;

        // Whitelist of allowed entities and their required scopes
        private static readonly Dictionary<string, string> EntityScopeMap = new()
        {
            { "Items", "items:read" },
            { "Movements", "items:read" },
            { "Machines", "machines:read" },
            { "MovReposiciones", "machines:read" },
            { "MovCargasDescargasMaquinas", "machines:read" },
            { "MasterMaquinaDispensacions", "machines:read" },
            { "MovCreditosDepartamentos", "machines:read" },
            { "MovMaquinaContadorPrendasSucias", "machines:read" },
            { "LogMaquinas", "audit:read" },
            { "LogErroresDispensacions", "audit:read" }
        };

        // Allowed fields per entity (for security - only expose safe fields)
        private static readonly Dictionary<string, string[]> AllowedFields = new()
        {
            { "Items", new[] { "Id", "RFIDCode", "Description", "LocationId", "LastMovementId", "DateLastMov", "Personalised", "FamilyItemModelId", "ItemSizeId" } },
            { "Movements", new[] { "Id", "Description", "CreationDate" } },
            { "Machines", new[] { "Id", "Description", "IdCliente", "CreationDate" } }
        };

        // Allowed relations per entity
        private static readonly Dictionary<string, string[]> AllowedRelations = new()
        {
            { "Items", new[] { "FamilyItemModel", "ItemSize", "Location" } }
        };

        public LookupService(DataContext context)
        {
            _context = context;
        }

        public async Task<LookupResponse> LookupAsync(LookupRequest request, string[] allowedScopes)
        {
            // Validate entity is whitelisted
            if (!EntityScopeMap.ContainsKey(request.Entity))
            {
                throw new ArgumentException($"Entity '{request.Entity}' is not allowed");
            }

            // Validate scope
            var requiredScope = EntityScopeMap[request.Entity];
            if (!allowedScopes.Contains(requiredScope))
            {
                throw new UnauthorizedAccessException($"Scope '{requiredScope}' is required for entity '{request.Entity}'");
            }

            // Validate fields
            if (AllowedFields.ContainsKey(request.Entity))
            {
                var invalidFields = request.Fields.Except(AllowedFields[request.Entity]).ToList();
                if (invalidFields.Any())
                {
                    throw new ArgumentException($"Fields not allowed: {string.Join(", ", invalidFields)}");
                }
            }

            // Validate relations
            if (request.IncludeRelated != null && AllowedRelations.ContainsKey(request.Entity))
            {
                var invalidRelations = request.IncludeRelated.Except(AllowedRelations[request.Entity]).ToList();
                if (invalidRelations.Any())
                {
                    throw new ArgumentException($"Relations not allowed: {string.Join(", ", invalidRelations)}");
                }
            }

            // Perform lookup based on entity type
            object? result = request.Entity switch
            {
                "Items" => await LookupItemAsync(request),
                "Movements" => await LookupMovementAsync(request),
                "Machines" => await LookupMachineAsync(request),
                _ => throw new ArgumentException($"Lookup not implemented for entity '{request.Entity}'")
            };

            return new LookupResponse
            {
                Entity = request.Entity,
                Result = result
            };
        }

        private async Task<object?> LookupItemAsync(LookupRequest request)
        {
            var query = _context.Items.AsQueryable();

            // Apply key field filter
            if (request.KeyField == "RFIDCode")
            {
                query = query.Where(i => i.RFIDCode == request.KeyValue && !i.Deleted);
            }
            else if (request.KeyField == "Id")
            {
                if (int.TryParse(request.KeyValue, out var id))
                {
                    query = query.Where(i => i.Id == id && !i.Deleted);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                throw new ArgumentException($"Key field '{request.KeyField}' not supported for Items");
            }

            // Include relations if requested
            if (request.IncludeRelated != null)
            {
                if (request.IncludeRelated.Contains("FamilyItemModel"))
                {
                    query = query.Include(i => i.FamilyItemModel);
                }
                if (request.IncludeRelated.Contains("ItemSize"))
                {
                    query = query.Include(i => i.ItemSize);
                }
                if (request.IncludeRelated.Contains("Location"))
                {
                    query = query.Include(i => i.Location);
                }
            }

            // Project to DTO with only allowed fields
            var item = await query.Select(i => new
            {
                Id = request.Fields.Contains("Id") ? i.Id : (int?)null,
                RFIDCode = request.Fields.Contains("RFIDCode") ? i.RFIDCode : null,
                Description = request.Fields.Contains("Description") ? i.Description : null,
                LocationId = request.Fields.Contains("LocationId") ? i.LocationId : null,
                LastMovementId = request.Fields.Contains("LastMovementId") ? i.LastMovementId : null,
                DateLastMov = request.Fields.Contains("DateLastMov") ? i.DateLastMov : null,
                Personalised = request.Fields.Contains("Personalised") ? i.Personalised : (bool?)null,
                FamilyItemModelId = request.Fields.Contains("FamilyItemModelId") ? i.FamilyItemModelId : (int?)null,
                ItemSizeId = request.Fields.Contains("ItemSizeId") ? i.ItemSizeId : null
            }).FirstOrDefaultAsync();

            return item;
        }

        private async Task<object?> LookupMovementAsync(LookupRequest request)
        {
            var query = _context.Movements.AsQueryable();

            if (request.KeyField == "Id")
            {
                if (int.TryParse(request.KeyValue, out var id))
                {
                    query = query.Where(m => m.Id == id && !m.Deleted);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                throw new ArgumentException($"Key field '{request.KeyField}' not supported for Movements");
            }

            var movement = await query.Select(m => new
            {
                Id = request.Fields.Contains("Id") ? m.Id : (int?)null,
                Description = request.Fields.Contains("Description") ? m.Description : null,
                CreationDate = request.Fields.Contains("CreationDate") ? m.CreationDate : (DateTime?)null
            }).FirstOrDefaultAsync();

            return movement;
        }

        private async Task<object?> LookupMachineAsync(LookupRequest request)
        {
            var query = _context.Machines.AsQueryable();

            if (request.KeyField == "Id")
            {
                if (int.TryParse(request.KeyValue, out var id))
                {
                    query = query.Where(m => m.Id == id && !m.Deleted);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                throw new ArgumentException($"Key field '{request.KeyField}' not supported for Machines");
            }

            var machine = await query.Select(m => new
            {
                Id = request.Fields.Contains("Id") ? m.Id : (int?)null,
                Description = request.Fields.Contains("Description") ? m.Description : null,
                IdCliente = request.Fields.Contains("IdCliente") ? m.IdCliente : null,
                CreationDate = request.Fields.Contains("CreationDate") ? m.CreationDate : (DateTime?)null
            }).FirstOrDefaultAsync();

            return machine;
        }
    }
}
