using LFS.InfraStructure;
using LFS.SecureAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LFS.SecureAPI.Services
{
    public class MasterDataService : IMasterDataService
    {
        private readonly DataContext _context;

        public MasterDataService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<ClientDto>> GetClientsAsync()
        {
            return await _context.MasterClientes
                .Where(c => !c.Borrado.HasValue || !c.Borrado.Value)
                .Select(c => new ClientDto
                {
                    Id = c.Id,
                    Nombre = c.Nombre
                })
                .ToListAsync();
        }

        public async Task<List<GarmentModelDto>> GetGarmentModelsAsync()
        {
            return await _context.MasterModeloPrenda
                .Select(m => new GarmentModelDto
                {
                    Id = m.Id,
                    Descripcion = m.Descripcion
                })
                .ToListAsync();
        }

        public async Task<List<AntennaTypeDto>> GetAntennaTypesAsync()
        {
            return await _context.MasterTipoAntenas
                .Where(a => a.Mostrar == true)
                .Select(a => new AntennaTypeDto
                {
                    Id = a.Id,
                    Descripcion = a.Descripcion
                })
                .ToListAsync();
        }
    }
}
