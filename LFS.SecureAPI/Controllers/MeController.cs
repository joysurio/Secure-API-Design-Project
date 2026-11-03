using LFS.SecureAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LFS.SecureAPI.Controllers
{
    /// <summary>
    /// User profile and preferences per CTO MVP spec.
    /// Size GET/PUT are registered as minimal APIs in MeSizesEndpoints (same pipeline as /api/health).
    /// </summary>
    [ApiController]
    [Route("api/me")]
    [Authorize(Policy = "RequireUserAuth")]
    public class MeController : ControllerBase
    {
        private readonly IMeService _meService;

        public MeController(IMeService meService)
        {
            _meService = meService;
        }

        private string? UserId => User.FindFirstValue("sub");
        private string? RequestLanguageCode
        {
            get
            {
                var raw = Request.Headers.AcceptLanguage.ToString();
                if (string.IsNullOrWhiteSpace(raw))
                    return null;
                var first = raw.Split(',')[0].Trim();
                if (string.IsNullOrWhiteSpace(first))
                    return null;
                var semi = first.IndexOf(';');
                if (semi >= 0)
                    first = first[..semi].Trim();
                return first;
            }
        }

        [HttpGet]
        public async Task<ActionResult<MeResponse>> GetProfile()
        {
            if (string.IsNullOrEmpty(UserId))
                return Unauthorized();
            try
            {
                var profile = await _meService.GetProfileAsync(UserId);
                return Ok(profile);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        [HttpGet("credits")]
        public async Task<ActionResult<MeCreditsResponse>> GetCredits()
        {
            if (string.IsNullOrEmpty(UserId))
                return Unauthorized();
            var credits = await _meService.GetCreditsAsync(UserId, RequestLanguageCode);
            return Ok(credits);
        }

        [HttpGet("machines/status")]
        public async Task<ActionResult<IReadOnlyList<MachineStatusDto>>> GetMachineStatus()
        {
            if (string.IsNullOrEmpty(UserId))
                return Unauthorized();
            try
            {
                var status = await _meService.GetMachineStatusAsync(UserId, RequestLanguageCode).ConfigureAwait(false);
                return Ok(status);
            }
            catch (Exception ex) when (ex is Microsoft.Data.SqlClient.SqlException or TimeoutException)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, new { error = "Machine status data is temporarily unavailable. Please try again." });
            }
        }

        [HttpGet("machines/{machineId:int}/warehouses")]
        public async Task<ActionResult<IReadOnlyList<MachineWarehouseStockDto>>> GetMachineWarehouses(int machineId)
        {
            if (string.IsNullOrEmpty(UserId))
                return Unauthorized();
            try
            {
                var rows = await _meService.GetMachineWarehouseStockAsync(UserId, machineId, RequestLanguageCode).ConfigureAwait(false);
                return Ok(rows);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }

        [HttpGet("machines/consumption")]
        public async Task<ActionResult<MachineConsumptionDto>> GetMachineConsumption([FromQuery] string period = "day")
        {
            if (string.IsNullOrEmpty(UserId))
                return Unauthorized();
            var me = await _meService.GetProfileAsync(UserId).ConfigureAwait(false);
            if (!string.Equals(me.Role, "admin", StringComparison.OrdinalIgnoreCase))
                return Forbid();
            var data = await _meService.GetMachineConsumptionAsync(UserId, period, RequestLanguageCode);
            return Ok(data);
        }

        /// <summary>Movements: <paramref name="from"/> is inclusive start of range; <paramref name="to"/> is exclusive end (first instant after the last selected calendar day). Omit <paramref name="modelId"/> for all models.</summary>
        [HttpGet("movements")]
        public async Task<ActionResult<MovementsResponse>> GetMovements(
            [FromQuery] DateTime? from,
            [FromQuery] DateTime? to,
            [FromQuery] string? type,
            [FromQuery] int? modelId,
            [FromQuery] int page = 1,
            [FromQuery] int size = 20)
        {
            if (string.IsNullOrEmpty(UserId))
                return Unauthorized();
            var result = await _meService.GetMovementsAsync(UserId, from, to, type, modelId, page, size, RequestLanguageCode);
            return Ok(result);
        }

        [HttpGet("admin/movement-types")]
        public async Task<ActionResult<IReadOnlyList<AdminMovementTypeDto>>> GetAdminMovementTypes()
        {
            if (string.IsNullOrEmpty(UserId))
                return Unauthorized();
            try
            {
                var types = await _meService.GetAdminMovementTypesAsync(UserId, RequestLanguageCode)
                    .ConfigureAwait(false);
                return Ok(types);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }

        [HttpGet("admin/movements")]
        public async Task<ActionResult<MovementsResponse>> GetAdminMovements(
            [FromQuery] DateTime? from,
            [FromQuery] DateTime? to,
            [FromQuery] int? machineId,
            [FromQuery] int? modelId,
            [FromQuery] string? talla,
            [FromQuery] int? userId,
            [FromQuery] string? type,
            [FromQuery] int page = 1,
            [FromQuery] int size = 20)
        {
            if (string.IsNullOrEmpty(UserId))
                return Unauthorized();
            try
            {
                var result = await _meService.GetAdminMovementsAsync(
                        UserId, from, to, machineId, modelId, talla, userId, type, page, size, RequestLanguageCode)
                    .ConfigureAwait(false);
                return Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }

        /// <summary>Admin: search users (<c>MasterSubClientes</c>) in the same client.</summary>
        [HttpGet("admin/subclientes")]
        public async Task<ActionResult<IReadOnlyList<AdminSubClienteSummaryDto>>> AdminSearchSubClientes(
            [FromQuery] string? search = null,
            [FromQuery] int take = 40)
        {
            if (string.IsNullOrEmpty(UserId))
                return Unauthorized();
            try
            {
                var list = await _meService.SearchSubClientesForAdminAsync(UserId, search, take).ConfigureAwait(false);
                return Ok(list);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }

        /// <summary>Admin: stock rows and limits for one sub-client.</summary>
        [HttpGet("admin/subclientes/{subClienteId:int}/credits")]
        public async Task<ActionResult<AdminUserCreditsDetailDto>> AdminGetSubClienteCredits(int subClienteId)
        {
            if (string.IsNullOrEmpty(UserId))
                return Unauthorized();
            try
            {
                var detail = await _meService.GetSubClienteCreditsDetailForAdminAsync(UserId, subClienteId, RequestLanguageCode)
                    .ConfigureAwait(false);
                if (detail == null)
                    return NotFound();
                return Ok(detail);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }

        /// <summary>
        /// Admin: credit history for one sub-client (<c>MovStockPrendasUsu_Historico</c>),
        /// filterable by date range and garment model.
        /// </summary>
        [HttpGet("admin/subclientes/{subClienteId:int}/credits/history")]
        public async Task<ActionResult<AdminCreditHistoryResponse>> AdminGetSubClienteCreditHistory(
            int subClienteId,
            [FromQuery] DateTime? from,
            [FromQuery] DateTime? to,
            [FromQuery] int? modelId,
            [FromQuery] int page = 1,
            [FromQuery] int size = 50)
        {
            if (string.IsNullOrEmpty(UserId))
                return Unauthorized();
            try
            {
                var result = await _meService.GetSubClienteCreditHistoryForAdminAsync(
                        UserId, subClienteId, from, to, modelId, page, size, RequestLanguageCode)
                    .ConfigureAwait(false);
                return Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("not found", StringComparison.OrdinalIgnoreCase))
            {
                return NotFound(new { error = ex.Message });
            }
        }

        /// <summary>Admin: update <c>MovStockPrendasUsu</c> and write history.</summary>
        [HttpPut("admin/subclientes/{subClienteId:int}/credits")]
        public async Task<IActionResult> AdminPutSubClienteCredit(int subClienteId, [FromBody] AdminCreditUpdateRequestDto? body)
        {
            if (string.IsNullOrEmpty(UserId))
                return Unauthorized();
            if (body == null)
                return BadRequest(new { error = "Body required." });
            try
            {
                await _meService.UpdateSubClienteCreditForAdminAsync(UserId, subClienteId, body).ConfigureAwait(false);
                return NoContent();
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
