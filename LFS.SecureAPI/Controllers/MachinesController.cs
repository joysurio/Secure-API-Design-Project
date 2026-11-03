using LFS.SecureAPI.Models;
using LFS.SecureAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LFS.SecureAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MachinesController : ControllerBase
    {
        private readonly IMachineService _machineService;

        public MachinesController(IMachineService machineService)
        {
            _machineService = machineService;
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "Scope:machines.read")]
        public async Task<ActionResult<MachineDto>> GetMachine(int id)
        {
            try
            {
                var machine = await _machineService.GetMachineAsync(id);
                return Ok(machine);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpGet("{id}/inventory")]
        [Authorize(Policy = "Scope:machines.read")]
        public async Task<ActionResult<MachineInventoryResponse>> GetMachineInventory(int id)
        {
            try
            {
                var inventory = await _machineService.GetMachineInventoryAsync(id);
                return Ok(inventory);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpPost("{id}/load")]
        [Authorize(Policy = "Scope:machines.write")]
        public async Task<ActionResult<MachineLoadResponse>> RegisterMachineLoad(int id, [FromBody] MachineLoadRequest request)
        {
            try
            {
                var response = await _machineService.RegisterMachineLoadAsync(id, request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPost("{id}/replenishment")]
        [Authorize(Policy = "Scope:machines.write")]
        public async Task<ActionResult<ReplenishmentResponse>> CreateReplenishment(int id, [FromBody] ReplenishmentRequest request)
        {
            try
            {
                var response = await _machineService.CreateReplenishmentAsync(id, request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPost("{id}/credits")]
        [Authorize(Policy = "Scope:machines.write")]
        public async Task<ActionResult<CreditsResponse>> ReplenishCredits(int id, [FromBody] CreditsRequest request)
        {
            try
            {
                var response = await _machineService.ReplenishCreditsAsync(id, request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPost("{id}/delivery")]
        [Authorize(Policy = "Scope:machines.write")]
        public async Task<ActionResult<DeliveryResponse>> RegisterDelivery(int id, [FromBody] DeliveryRequest request)
        {
            try
            {
                var response = await _machineService.RegisterDeliveryAsync(id, request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
