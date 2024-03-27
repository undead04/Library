using Library.Data;
using Library.Model;
using OfficeOpenXml.Sorting;

namespace Library.Services.NotificationRepository
{
    public interface INotificationRepository
    {
        Task<int> CreateNotification(NotificationModel model);
        Task DeleteNotification(int id);
        Task<Notification> GetNotification(int id);
    }
}
