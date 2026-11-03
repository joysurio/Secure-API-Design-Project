using LFS.SecureAPI.Models;
using LFS.SecureAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LFS.SecureAPI.Controllers
{
    [ApiController]
    [Route("api/support")]
    [Authorize(Policy = "RequireUserAuth")]
    public class SupportController : ControllerBase
    {
        private readonly ISupportTicketService _supportTicketService;

        public SupportController(ISupportTicketService supportTicketService)
        {
            _supportTicketService = supportTicketService;
        }

        [HttpPost("tickets")]
        public async Task<ActionResult> CreateTicket([FromBody] SupportTicketRequest request)
        {
            var userId = User.FindFirstValue("sub");
            if (string.IsNullOrWhiteSpace(userId))
                return Unauthorized();

            var id = await _supportTicketService.CreateTicketAsync(userId, request);
            return Ok(new { id, status = "open" });
        }

        [HttpGet("tickets")]
        public async Task<ActionResult<IReadOnlyList<SupportTicketAdminDto>>> GetTickets([FromQuery] string? status = "all")
        {
            var userId = User.FindFirstValue("sub");
            if (string.IsNullOrWhiteSpace(userId))
                return Unauthorized();

            try
            {
                var rows = await _supportTicketService.GetAdminTicketsAsync(userId, status).ConfigureAwait(false);
                return Ok(rows);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }

        [HttpPost("tickets/{ticketGuid}/resolve")]
        public async Task<ActionResult> ResolveTicket(string ticketGuid)
        {
            var userId = User.FindFirstValue("sub");
            if (string.IsNullOrWhiteSpace(userId))
                return Unauthorized();

            try
            {
                await _supportTicketService.ResolveTicketAsync(userId, ticketGuid).ConfigureAwait(false);
                return NoContent();
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }
    }
}
