using LFS.SecureAPI.Models;
using LFS.SecureAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LFS.SecureAPI.Controllers
{
    [ApiController]
    [Route("api/notifications")]
    [Authorize(Policy = "RequireUserAuth")]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] NotificationRegisterRequest request)
        {
            var userId = User.FindFirstValue("sub");
            if (string.IsNullOrWhiteSpace(userId))
                return Unauthorized();

            await _notificationService.RegisterDeviceTokenAsync(userId, request);
            return Ok(new { message = "Device token registered" });
        }
    }
}
