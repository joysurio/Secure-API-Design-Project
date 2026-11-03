using LFS.SecureAPI.Models;

namespace LFS.SecureAPI.Services
{
    public interface IMasterDataService
    {
        Task<List<ClientDto>> GetClientsAsync();
        Task<List<GarmentModelDto>> GetGarmentModelsAsync();
        Task<List<AntennaTypeDto>> GetAntennaTypesAsync();
    }
}
