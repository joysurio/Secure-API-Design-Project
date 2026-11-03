using LFS.InfraStructure;
using LFS.Shared.Entities;
using LFS.SecureAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LFS.SecureAPI.Services
{
    public class MovPrenService : IMovPrenService
    {
        private readonly DataContext _context;

        public MovPrenService(DataContext context)
        {
            _context = context;
        }

        public async Task<InsertMovPrenResponse> InsertMovPrenAsync(InsertMovPrenRequest request)
        {
            // Validate idCli exists
            var cliente = await _context.MasterClientes.FindAsync(request.IdCli);
            if (cliente == null)
            {
                throw new ArgumentException($"idCli {request.IdCli} does not exist");
            }

            // Validate idTipoAntena exists
            var tipoAntena = await _context.MasterTipoAntenas.FindAsync(request.IdTipoAntena);
            if (tipoAntena == null)
            {
                throw new ArgumentException($"idTipoAntena {request.IdTipoAntena} does not exist");
            }

            // Validate idModeloPrenda exists
            var modeloPrenda = await _context.MasterModeloPrenda.FindAsync(request.IdModeloPrenda);
            if (modeloPrenda == null)
            {
                throw new ArgumentException($"idModeloPrenda {request.IdModeloPrenda} does not exist");
            }

            // Validate talla if provided
            if (!string.IsNullOrEmpty(request.Talla) && request.Talla.Length > 50)
            {
                throw new ArgumentException("Talla value is too long (max 50 characters)");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Get IdPrenda by codigoTAG
                var prenda = await _context.MasterPrendas
                    .FirstOrDefaultAsync(p => p.CodigoTag == request.CodigoTAG && (!p.Borrado.HasValue || !p.Borrado.Value));

                bool prendaCreated = false;

                // If prenda not found, create it
                if (prenda == null)
                {
                    prenda = await InsertarPrendaAsync(request);
                    prendaCreated = true;
                }

                // Get idSubCliente (generic subclient)
                var idSubCliente = await ObtenerIdSubClienteAsync(request.IdCli);

                // Create MovPren
                var movPren = new MovPren
                {
                    IdPrenda = prenda.Id,
                    Fecha = DateTime.UtcNow,
                    Hora = DateTime.UtcNow,
                    IdPuesto = request.IdPuesto,
                    IdTipoAntena = request.IdTipoAntena,
                    IdCli = request.IdCli,
                    IdSubCli = idSubCliente,
                    IdModeloPrenda = request.IdModeloPrenda,
                    Talla = request.Talla
                };

                _context.MovPrens.Add(movPren);
                await _context.SaveChangesAsync();

                // If id_tipoAntena is 2, increment NCiclos in MasterPrendas
                if (request.IdTipoAntena == 2)
                {
                    prenda.Nciclos = (prenda.Nciclos ?? 0) + 1;
                    _context.MasterPrendas.Update(prenda);
                    await _context.SaveChangesAsync();
                }

                await transaction.CommitAsync();

                return new InsertMovPrenResponse
                {
                    Id = movPren.Id,
                    IdPrenda = prenda.Id,
                    PrendaCreated = prendaCreated,
                    Message = prendaCreated ? "Movement created and garment inserted" : "Movement created successfully"
                };
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        private async Task<MasterPrendas> InsertarPrendaAsync(InsertMovPrenRequest request)
        {
            // Get idSubCliente (generic)
            var idSubCliente = await ObtenerIdSubClienteAsync(request.IdCli);

            // Get model description
            var modelo = await _context.MasterModeloPrenda.FindAsync(request.IdModeloPrenda);
            if (modelo == null)
            {
                throw new InvalidOperationException($"Model {request.IdModeloPrenda} not found");
            }

            var descripcion = modelo.Descripcion;
            var maxLavados = modelo.MaxLavados ?? 0;

            // Create new prenda
            var prenda = new MasterPrendas
            {
                IdModeloPrenda = request.IdModeloPrenda,
                IdCliente = request.IdCli,
                IdSubCliente = idSubCliente,
                CodigoTag = request.CodigoTAG,
                Descrip = descripcion,
                Falta = DateTime.UtcNow,
                Nueva = false,
                Nlavados = maxLavados,
                Accesorio = false,
                Talla = request.Talla,
                IdEtiqueta = 0,
                Borrado = false,
                Nciclos = 0,
                Norden = 0,
                Ntotal = 0,
                Stock = 0,
                Facturable = true,
                Personalizada = false,
                ComprobacionLecturas = 0,
                ModeloComprobacion = 0,
                NoActualizarModeloTalla = true
            };

            _context.MasterPrendas.Add(prenda);
            await _context.SaveChangesAsync();

            return prenda;
        }

        private async Task<int?> ObtenerIdSubClienteAsync(int idCliente)
        {
            var subCliente = await _context.MasterSubClientes
                .FirstOrDefaultAsync(s => s.IdCliente == idCliente && s.Generico == true && (!s.Borrado.HasValue || !s.Borrado.Value));

            return subCliente?.Id;
        }
    }
}
