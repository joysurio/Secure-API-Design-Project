using LFS.SecureAPI.Models;
using LFS.SecureAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LFS.SecureAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OfflineController : ControllerBase
    {
        private readonly IOfflineSyncService _offlineSyncService;

        public OfflineController(IOfflineSyncService offlineSyncService)
        {
            _offlineSyncService = offlineSyncService;
        }

        [HttpPost("sync")]
        [Authorize(Policy = "Scope:offline.sync")]
        public async Task<ActionResult<OfflineSyncResponse>> Sync([FromBody] OfflineSyncRequest request)
        {
            try
            {
                var response = await _offlineSyncService.SyncOfflineOperationsAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
