using LFS.SecureAPI.Models;
using LFS.SecureAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LFS.SecureAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpPost("counts")]
        [Authorize(Policy = "Scope:items.write")]
        public async Task<ActionResult<InventoryCountResponse>> SubmitInventoryCounts([FromBody] InventoryCountRequest request)
        {
            try
            {
                var response = await _inventoryService.SubmitInventoryCountsAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("reconcile/{locationId}")]
        [Authorize(Policy = "Scope:items.read")]
        public async Task<ActionResult<InventoryReconcileResponse>> ReconcileInventory(int locationId)
        {
            var response = await _inventoryService.ReconcileInventoryAsync(locationId);
            return Ok(response);
        }
    }
}
