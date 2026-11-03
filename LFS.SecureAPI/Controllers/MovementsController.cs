using LFS.SecureAPI.Models;
using LFS.SecureAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LFS.SecureAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MovementsController : ControllerBase
    {
        private readonly IMovementService _movementService;
        private readonly IMovPrenService _movPrenService;

        public MovementsController(IMovementService movementService, IMovPrenService movPrenService)
        {
            _movementService = movementService;
            _movPrenService = movPrenService;
        }

        [HttpPost]
        [Authorize(Policy = "Scope:items.write")]
        public async Task<ActionResult<MovementResponse>> CreateMovement([FromBody] CreateMovementRequest request)
        {
            try
            {
                var response = await _movementService.CreateMovementAsync(request);
                return CreatedAtAction(nameof(CreateMovement), new { id = response.Id }, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPost("insert")]
        [Authorize(Policy = "Scope:items.write")]
        public async Task<ActionResult<InsertMovPrenResponse>> InsertMovPren([FromBody] InsertMovPrenRequest request)
        {
            try
            {
                var response = await _movPrenService.InsertMovPrenAsync(request);
                return StatusCode(201, response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
