using LFS.InfraStructure;
using LFS.Shared.Entities;
using LFS.SecureAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LFS.SecureAPI.Services
{
    public class MachineService : IMachineService
    {
        private readonly DataContext _context;

        public MachineService(DataContext context)
        {
            _context = context;
        }

        public async Task<MachineDto> GetMachineAsync(int id)
        {
            var machine = await _context.Machines
                .FirstOrDefaultAsync(m => m.Id == id && !m.Deleted);

            if (machine == null)
            {
                throw new KeyNotFoundException($"Machine with ID {id} not found");
            }

            return new MachineDto
            {
                Id = machine.Id,
                Description = machine.Description,
                IdCliente = machine.IdCliente,
                CreationDate = machine.CreationDate
            };
        }

        public async Task<MachineInventoryResponse> GetMachineInventoryAsync(int id)
        {
            var machine = await _context.MasterMaquinaDispensacions
                .Include(m => m.MovMaquinaDisAlmacenes)
                .Include(m => m.MovMaquinaDepartamentos)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (machine == null)
            {
                throw new KeyNotFoundException($"Machine with ID {id} not found");
            }

            return new MachineInventoryResponse
            {
                MachineId = id,
                Almacenes = machine.MovMaquinaDisAlmacenes.Select(a => new AlmacenDto
                {
                    Id = a.Id,
                    Stock = a.StockReal,
                    MaximumQuantity = a.CantMax
                }).ToList(),
                Departamentos = machine.MovMaquinaDepartamentos.Select(d => new DepartamentoDto
                {
                    IdDepartamento = d.IdMasterDepartamentosCliente
                }).ToList()
            };
        }

        public async Task<MachineLoadResponse> RegisterMachineLoadAsync(int id, MachineLoadRequest request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var cargaDescarga = new MovCargasDescargasMaquina
                {
                    TipoMov = request.TipoMov, // 1 = carga, 2 = descarga
                    IdMaquina = id,
                    IdUsuario = request.UserId,
                    FechaHora = DateTime.UtcNow
                };

                _context.MovCargasDescargasMaquinas.Add(cargaDescarga);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return new MachineLoadResponse
                {
                    Id = cargaDescarga.Id,
                    MachineId = id,
                    TipoMov = request.TipoMov,
                    ProcessedAt = DateTime.UtcNow
                };
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<ReplenishmentResponse> CreateReplenishmentAsync(int id, ReplenishmentRequest request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var reposicion = new MovReposicione
                {
                    IdRep = Guid.NewGuid(),
                    PuntoRep = request.PuntoRep,
                    ModeloRep = request.ModeloRep,
                    CantidadRep = request.CantidadRep,
                    FechaRep = DateTime.UtcNow,
                    UsuarioRep = request.UsuarioRep,
                    PuestoRep = request.PuestoRep,
                    SolicitadasRep = request.SolicitadasRep,
                    RepuestoRep = false,
                    AlmacenRep = id,
                    CreationDate = DateTime.UtcNow
                };

                _context.MovReposiciones.Add(reposicion);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return new ReplenishmentResponse
                {
                    IdRep = reposicion.IdRep,
                    MachineId = id,
                    ProcessedAt = DateTime.UtcNow
                };
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<CreditsResponse> ReplenishCreditsAsync(int id, CreditsRequest request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Update or create MovCreditosDepartamentos
                var credito = await _context.MovCreditosDepartamentos
                    .FirstOrDefaultAsync(c => c.IdDepartamento == request.IdDepartamento);

                if (credito == null)
                {
                    credito = new MovCreditosDepartamentos
                    {
                        IdDepartamento = request.IdDepartamento,
                        CantMaxPrendasUso = request.CantMaxPrendasUso,
                        LimitacionHoras = request.LimitacionHoras,
                        RangoCredito = request.RangoCredito
                    };
                    _context.MovCreditosDepartamentos.Add(credito);
                }
                else
                {
                    credito.CantMaxPrendasUso = request.CantMaxPrendasUso;
                    credito.LimitacionHoras = request.LimitacionHoras;
                    credito.RangoCredito = request.RangoCredito;
                    _context.MovCreditosDepartamentos.Update(credito);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return new CreditsResponse
                {
                    Id = credito.Id,
                    IdDepartamento = credito.IdDepartamento,
                    ProcessedAt = DateTime.UtcNow
                };
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<DeliveryResponse> RegisterDeliveryAsync(int id, DeliveryRequest request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Create movement
                var movement = new Movement
                {
                    Description = $"Machine {id} delivery: {request.Description}",
                    CreationDate = DateTime.UtcNow,
                    Deleted = false
                };
                _context.Movements.Add(movement);
                await _context.SaveChangesAsync();

                // Update items if provided
                if (request.ItemIds != null && request.ItemIds.Any())
                {
                    var items = await _context.Items
                        .Where(i => request.ItemIds.Contains(i.Id))
                        .ToListAsync();

                    foreach (var item in items)
                    {
                        item.LastMovementId = movement.Id;
                        item.DateLastMov = DateTime.UtcNow;
                    }
                    _context.Items.UpdateRange(items);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return new DeliveryResponse
                {
                    MovementId = movement.Id,
                    MachineId = id,
                    ProcessedAt = DateTime.UtcNow
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
