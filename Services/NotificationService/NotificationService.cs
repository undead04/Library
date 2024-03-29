using Library.Model;
using Library.Repository.MyNotificationRepository;
using Library.Repository.NotificationRepository;

namespace Library.Services.NotificationService
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository notificationRepository;
        private readonly IMyNotificationRepository myNotificationRepository;

        public NotificationService(INotificationRepository notificationRepository,IMyNotificationRepository myNotificationRepository) 
        { 
            this.notificationRepository=notificationRepository;
            this.myNotificationRepository = myNotificationRepository;
        }
        public async Task CreateNotification(string type, string content, List<string> receiveUserId, string sendUserId)
        {
            var notificationid =await notificationRepository.CreateNotification(type,content, sendUserId);
            await myNotificationRepository.CreateMyNotification(receiveUserId, notificationid);

        }
    }
}
