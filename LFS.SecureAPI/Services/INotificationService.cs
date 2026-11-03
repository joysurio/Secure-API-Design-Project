using LFS.SecureAPI.Models;

namespace LFS.SecureAPI.Services
{
    public interface INotificationService
    {
        Task RegisterDeviceTokenAsync(string userId, NotificationRegisterRequest request);
    }
}
