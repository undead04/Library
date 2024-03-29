using DocumentFormat.OpenXml.VariantTypes;
using Library.Data;
using Library.Model;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository.NotificationRepository
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly MyDB context;

        public NotificationRepository(MyDB context)
        {
            this.context = context;
        }
        public async Task<int> CreateNotification(string typeNotification,string content, string userId)
        {
            var notification = new Notification
            {
                UserId = userId,
                Content = content,
                CreateDate = DateTime.Now,
                TypeNotification = typeNotification
            };
            await context.notifications.AddAsync(notification);
            await context.SaveChangesAsync();
            return notification.Id;
        }

        public async Task DeleteNotification(int id)
        {
            var notification = await context.notifications.FirstOrDefaultAsync(no => no.Id == id);
            if (notification != null)
            {
                context.Remove(notification);
                await context.SaveChangesAsync();
            }
        }

        public async Task<Notification> GetNotification(int id)
        {
            var notification = await context.notifications.FirstOrDefaultAsync(no => no.Id == id);
            if (notification != null)
            {
                return notification;
            }
            return null;
        }
    }
}
