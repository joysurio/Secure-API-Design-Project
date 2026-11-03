using LFS.SecureAPI.Models;
using LFS.SecureAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LFS.SecureAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ItemsController : ControllerBase
    {
        private readonly IMovementService _movementService;

        public ItemsController(IMovementService movementService)
        {
            _movementService = movementService;
        }

        [HttpPost("{itemId}/movement")]
        [Authorize(Policy = "Scope:items.write")]
        public async Task<ActionResult<MovementResponse>> CreateItemMovement(int itemId, [FromBody] CreateItemMovementRequest request)
        {
            try
            {
                var response = await _movementService.CreateItemMovementAsync(itemId, request);
                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
