using LFS.SecureAPI.Models;
using LFS.SecureAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LFS.SecureAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MasterDataController : ControllerBase
    {
        private readonly IMasterDataService _masterDataService;

        public MasterDataController(IMasterDataService masterDataService)
        {
            _masterDataService = masterDataService;
        }

        [HttpGet("clients")]
        [Authorize(Policy = "Scope:items.read")]
        public async Task<ActionResult<List<ClientDto>>> GetClients()
        {
            var clients = await _masterDataService.GetClientsAsync();
            return Ok(clients);
        }

        [HttpGet("garment-models")]
        [Authorize(Policy = "Scope:items.read")]
        public async Task<ActionResult<List<GarmentModelDto>>> GetGarmentModels()
        {
            var models = await _masterDataService.GetGarmentModelsAsync();
            return Ok(models);
        }

        [HttpGet("antenna-types")]
        [Authorize(Policy = "Scope:items.read")]
        public async Task<ActionResult<List<AntennaTypeDto>>> GetAntennaTypes()
        {
            var types = await _masterDataService.GetAntennaTypesAsync();
            return Ok(types);
        }
    }
}
