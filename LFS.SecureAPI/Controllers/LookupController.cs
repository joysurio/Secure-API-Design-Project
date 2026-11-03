using LFS.SecureAPI.Models;
using LFS.SecureAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LFS.SecureAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LookupController : ControllerBase
    {
        private readonly ILookupService _lookupService;

        public LookupController(ILookupService lookupService)
        {
            _lookupService = lookupService;
        }

        [HttpPost]
        public async Task<ActionResult<LookupResponse>> Lookup([FromBody] LookupRequest request)
        {
            try
            {
                // Get scopes from token
                var scopeClaim = User.FindFirst("scope")?.Value ?? string.Empty;
                var allowedScopes = scopeClaim.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                var response = await _lookupService.LookupAsync(request, allowedScopes);
                
                if (response.Result == null)
                {
                    return NotFound(new { error = "Entity not found" });
                }

                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
