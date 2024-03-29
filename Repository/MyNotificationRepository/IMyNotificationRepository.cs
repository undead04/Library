using Library.Data;
using Library.Model.DTO;

namespace Library.Repository.MyNotificationRepository
{
    public interface IMyNotificationRepository
    {
        Task CreateMyNotification(List<string> userId, int notificationId);
        Task DeleteNotification(string userId, int notificationId);
        Task ReadNotification(string userId, int notificationId);
        Task<List<NotificationDTO>> ListNotification(string userId, bool? isRead);
        Task<MyNotification> GetNotification(string userId, int notificationId);
        IQueryable<MyNotification> IsTrueOrFalse(IQueryable<MyNotification> notification,bool isSystem, string type);
    }
}
