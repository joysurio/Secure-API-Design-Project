using LFS.SecureAPI.Models;
using LFS.SecureAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LFS.SecureAPI.Controllers
{
    /// <summary>
    /// Transactions (collect/return) per CTO MVP spec. Atomic operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "RequireUserAuth")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        private string? UserId => User.FindFirstValue("sub");

        [HttpPost]
        public async Task<ActionResult<TransactionResponse>> CreateTransaction([FromBody] TransactionRequest request)
        {
            if (string.IsNullOrEmpty(UserId))
                return Unauthorized();
            try
            {
                var response = await _transactionService.CreateTransactionAsync(UserId, request);
                return CreatedAtAction(nameof(CreateTransaction), response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpPost("{id}/cancel")]
        public async Task<ActionResult<TransactionResponse>> CancelTransaction([FromRoute] string id)
        {
            if (string.IsNullOrEmpty(UserId))
                return Unauthorized();
            try
            {
                var response = await _transactionService.CancelTransactionAsync(UserId, id);
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }
    }
}
