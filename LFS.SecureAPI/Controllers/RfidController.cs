using LFS.SecureAPI.Models;
using LFS.SecureAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LFS.SecureAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RfidController : ControllerBase
    {
        private readonly IRfidService _rfidService;

        public RfidController(IRfidService rfidService)
        {
            _rfidService = rfidService;
        }

        [HttpGet("item/{rfid}")]
        [Authorize(Policy = "Scope:rfid.read")]
        public async Task<ActionResult<ItemDto>> GetItemByRfid(string rfid)
        {
            var item = await _rfidService.GetItemByRfidAsync(rfid);
            if (item == null)
            {
                return NotFound(new { error = "Item not found" });
            }
            return Ok(item);
        }

        [HttpPost("scan")]
        [Authorize(Policy = "Scope:rfid.read")]
        public async Task<ActionResult<RfidScanResponse>> ScanRfidTags([FromBody] RfidScanRequest request)
        {
            try
            {
                var response = await _rfidService.ScanRfidTagsAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
