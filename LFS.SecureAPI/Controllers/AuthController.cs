using LFS.SecureAPI.Models;
using LFS.SecureAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LFS.SecureAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserAuthService _userAuthService;
        private readonly ISubClienteAuthService _subClienteAuthService;
        private readonly IAuthSessionService _authSessionService;
        private readonly IOptions<MobileQrPairingOptions> _mobileQrPairing;

        public AuthController(
            IAuthService authService,
            IUserAuthService userAuthService,
            ISubClienteAuthService subClienteAuthService,
            IAuthSessionService authSessionService,
            IOptions<MobileQrPairingOptions> mobileQrPairing)
        {
            _authService = authService;
            _userAuthService = userAuthService;
            _subClienteAuthService = subClienteAuthService;
            _authSessionService = authSessionService;
            _mobileQrPairing = mobileQrPairing;
        }

        /// <summary>
        /// User login (email + password) per CTO MVP spec.
        /// </summary>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
        {
            try
            {
                var response = await _userAuthService.LoginAsync(request);
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { error = ex.Message });
            }
        }

        /// <summary>
        /// SubCliente (employee) login via MasterSubClientes Id, DNI, or IDEmpleado + Campo2 password.
        /// Size endpoints: GET/PUT /api/subcliente/garment-sizes or /api/me/sizes (minimal APIs in Program pipeline).
        /// </summary>
        [HttpPost("subcliente/login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponse>> SubClienteLogin([FromBody] SubClienteLoginRequest request)
        {
            try
            {
                var response = await _subClienteAuthService.LoginAsync(request);
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Mobile QR onboarding: issue tokens for a SubCliente identified by user Id / DNI / IDEmpleado (no password).
        /// Configure <c>MobileQrPairing</c> in appsettings; optional <c>SharedSecret</c> via header <c>X-LFS-Mobile-Qr-Secret</c> or body <c>pairingSecret</c>.
        /// </summary>
        [HttpPost("subcliente/qr-pair")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponse>> SubClienteQrPair([FromBody] SubClienteQrPairRequest request)
        {
            var opts = _mobileQrPairing.Value;
            if (!opts.Enabled)
                return NotFound(new { error = "QR pairing is disabled." });

            if (!string.IsNullOrEmpty(opts.SharedSecret))
            {
                var header = Request.Headers["X-LFS-Mobile-Qr-Secret"].FirstOrDefault();
                var secretOk = string.Equals(header, opts.SharedSecret, StringComparison.Ordinal)
                    || string.Equals(request.PairingSecret, opts.SharedSecret, StringComparison.Ordinal);
                if (!secretOk)
                    return Unauthorized(new { error = "Invalid or missing pairing secret." });
            }

            try
            {
                var response = await _subClienteAuthService.QrPairAsync(request);
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { error = ex.Message });
            }
        }

        [HttpPost("device/register")]
        [AllowAnonymous] // Allow anonymous for initial device registration
        public async Task<ActionResult<DeviceRegistrationResponse>> RegisterDevice([FromBody] DeviceRegistrationRequest request)
        {
            try
            {
                var response = await _authService.RegisterDeviceAsync(request);
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("token")]
        [AllowAnonymous] // Token endpoint doesn't require authentication
        public async Task<ActionResult<TokenResponse>> GetToken([FromBody] TokenRequest request)
        {
            try
            {
                var response = await _authService.GetTokenAsync(request);
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { error = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("revoke")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> RevokeDevice([FromBody] RevokeDeviceRequest request)
        {
            var result = await _authService.RevokeDeviceAsync(request.DeviceId);
            if (result)
            {
                return Ok(new { message = "Device revoked successfully" });
            }
            return NotFound(new { error = "Device not found" });
        }

        [HttpPost("refresh")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponse>> Refresh([FromBody] RefreshRequest request)
        {
            try
            {
                var response = await _authSessionService.RefreshAsync(request.RefreshToken);
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { error = ex.Message });
            }
        }

        [HttpPost("logout")]
        [AllowAnonymous]
        public async Task<ActionResult> Logout([FromBody] LogoutRequest request)
        {
            await _authSessionService.LogoutAsync(request.RefreshToken);
            return Ok(new { message = "Logged out" });
        }
    }
}
