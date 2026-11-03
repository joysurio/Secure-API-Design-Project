using LFS.SecureAPI.Models;
using LFS.SecureAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LFS.SecureAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LinenController : ControllerBase
    {
        private readonly ILinenService _linenService;

        public LinenController(ILinenService linenService)
        {
            _linenService = linenService;
        }

        /// <summary>
        /// Step 1: Register new linen tags (Tag registration by Datamars handheld & tabletop)
        /// </summary>
        [HttpPost("register-tags")]
        [Authorize(Policy = "Scope:items.write")]
        public async Task<ActionResult<TagRegistrationResponse>> RegisterTags([FromBody] TagRegistrationRequest request)
        {
            try
            {
                var response = await _linenService.RegisterTagsAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Step 2: Deliver linen to user/sectors (Linen selected and scanned, 'Delivered' button activated)
        /// </summary>
        [HttpPost("deliver")]
        [Authorize(Policy = "Scope:items.write")]
        public async Task<ActionResult<LinenDeliveryResponse>> DeliverLinen([FromBody] LinenDeliveryRequest request)
        {
            try
            {
                var response = await _linenService.DeliverLinenAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Step 3: Mark linen as abnormal (When linen checked and found abnormal)
        /// </summary>
        [HttpPost("mark-abnormal")]
        [Authorize(Policy = "Scope:items.write")]
        public async Task<ActionResult<AbnormalMarkingResponse>> MarkAbnormal([FromBody] AbnormalMarkingRequest request)
        {
            try
            {
                var response = await _linenService.MarkAbnormalAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Step 5: Import washed items from laundry company (File import into software)
        /// </summary>
        [HttpPost("import-washed-items")]
        [Authorize(Policy = "Scope:items.write")]
        public async Task<ActionResult<WashedItemsImportResponse>> ImportWashedItems([FromBody] WashedItemsImportRequest request)
        {
            try
            {
                var response = await _linenService.ImportWashedItemsAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// Step 6: Random check for washed linen (Counter-check quantity from RFID list)
        /// </summary>
        [HttpPost("random-check")]
        [Authorize(Policy = "Scope:items.write")]
        public async Task<ActionResult<RandomCheckResponse>> RandomCheck([FromBody] RandomCheckRequest request)
        {
            try
            {
                var response = await _linenService.RandomCheckAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
