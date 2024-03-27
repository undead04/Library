using Library.Data;
using Library.DTO;
using Microsoft.EntityFrameworkCore;

namespace Library.Services.MyNotificationRepository
{
    public class MyNotificationRepository : IMyNotificationRepository
    {
        private readonly MyDB context;

        public MyNotificationRepository(MyDB context) 
        {
            this.context = context;
        }
        public async Task CreateMyNotification(string userId, int notificationId)
        {
            var myNotification = new MyNotification
            {
                IsRead = false,
                UserId = userId,
                NotificationId = notificationId
            };
            await context.myNotifications.AddAsync(myNotification);
            await context.SaveChangesAsync();
        }

        public async Task DeleteNotification(string userId, int notificationId)
        {
            var myNotification = await context.myNotifications.FirstOrDefaultAsync(no => no.UserId == userId && no.NotificationId == notificationId);
            if (myNotification != null)
            {
                context.Remove(myNotification);
                await context.SaveChangesAsync();
            }
        }

        public async Task<MyNotification> GetNotification(string userId, int notificationId)
        {
            var notification = await context.myNotifications.FirstOrDefaultAsync(no => no.UserId == userId && no.NotificationId == notificationId);
            if (notification != null)
            {
                return notification;
            }
            return null;
        }

        public async Task<List<NotificationDTO>> ListNotification(string userId,bool? isRead)
        {
            var notification = await context.myNotifications.Include(f=>f.Notification).Where(no => no.UserId == userId).ToListAsync();
            if(isRead.HasValue)
            {
                notification = notification.Where(no => no.IsRead == isRead).ToList();
            }
            return notification.Select(no => new NotificationDTO
            {
                NotificationId=no.NotificationId,
                UserId=no.Notification!.UserId,
                Content=no.Notification!.Content,
                CreateDate=no.Notification!.CreateDate,
                IsRead=no.IsRead
            }).ToList();
        }

        public async Task ReadNotification(string userId, int notificationId)
        {
            var notification = await context.myNotifications.FirstOrDefaultAsync(no=>no.UserId==userId && no.NotificationId==notificationId);
            if(notification!=null)
            {
                notification.IsRead = !notification.IsRead;
                await context.SaveChangesAsync();
            }
        }
    }
}
