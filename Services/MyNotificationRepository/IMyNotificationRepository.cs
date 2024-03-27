using Library.Data;
using Library.DTO;

namespace Library.Services.MyNotificationRepository
{
    public interface IMyNotificationRepository
    {
        Task CreateMyNotification(string userId, int notificationId);
        Task DeleteNotification(string userId, int notificationId);
        Task ReadNotification(string userId, int notificationId);
        Task<List<NotificationDTO>> ListNotification(string userId,bool?isRead);
        Task<MyNotification> GetNotification(string userId,int notificationId);
    }
}
