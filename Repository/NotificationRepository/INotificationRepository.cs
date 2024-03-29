using Library.Data;
using Library.Model;
using OfficeOpenXml.Sorting;

namespace Library.Repository.NotificationRepository
{
    public interface INotificationRepository
    {
        Task<int> CreateNotification(string typeNotification,string content, string userId);
        Task DeleteNotification(int id);
        Task<Notification> GetNotification(int id);
    }
}
