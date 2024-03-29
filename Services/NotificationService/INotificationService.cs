using Library.Data;
using Library.Model;

namespace Library.Services.NotificationService
{
    public interface INotificationService
    {
        Task CreateNotification(string type,string content,List<string> receiveUserId,string sendUserId);
    }
}
