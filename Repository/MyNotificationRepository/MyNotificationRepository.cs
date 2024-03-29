using Library.Data;
using Library.Model.DTO;
using Library.Repository.SystemNotificationRepository;
using Library.Services.UploadService;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository.MyNotificationRepository
{
    public class MyNotificationRepository : IMyNotificationRepository
    {
        private readonly MyDB context;
        private readonly ISystemNotificationRepository systemNotificationRepository;
        private readonly IUploadService uploadService;

        public MyNotificationRepository(MyDB context,ISystemNotificationRepository systemNotificationRepository,IUploadService uploadService)
        {
            this.context = context;
            this.systemNotificationRepository = systemNotificationRepository;
            this.uploadService = uploadService;
        }
        public async Task CreateMyNotification(List<string> userId, int notificationId)
        {
            foreach (var item in userId)
            {
                var myNotification = new MyNotification
                {
                    IsRead = false,
                    UserId = item,
                    NotificationId = notificationId
                };
                await context.myNotifications.AddAsync(myNotification);
                await context.SaveChangesAsync();
            }
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

        public async Task<List<NotificationDTO>> ListNotification(string userId, bool? isRead)
        {
            var systemNotification=await systemNotificationRepository.GetSystemNotification(userId);
            
            var notification =context.myNotifications.Include(f => f.Notification).Include(f=>f.User).AsQueryable();
            notification = notification.Where(no => no.UserId == userId);
            notification = IsTrueOrFalse(notification, systemNotification.IsChangePassword, TypeNotification.IsChangePassword);
            notification = IsTrueOrFalse(notification, systemNotification.IsUpdateInformationUser, TypeNotification.IsUpdateInformationUser);
            notification = IsTrueOrFalse(notification, systemNotification.IsChangleListRole, TypeNotification.IsChangleListRole);
            notification = IsTrueOrFalse(notification, systemNotification.IsChangeLeListUser, TypeNotification.IsChangeLeListUser);
            notification = IsTrueOrFalse(notification, systemNotification.IsSaveExam, TypeNotification.IsSaveExam);
            notification = IsTrueOrFalse(notification, systemNotification.IsCancelExam, TypeNotification.IsCancelExam);
            notification = IsTrueOrFalse(notification, systemNotification.IsCrudPrivateFile, TypeNotification.IsCrudPrivateFile);
            notification = IsTrueOrFalse(notification, systemNotification.IsContentSubject, TypeNotification.IsContentSubject);
            notification = IsTrueOrFalse(notification, systemNotification.IsListSUbject, TypeNotification.IsListSUbject);
            notification = IsTrueOrFalse(notification, systemNotification.IsCreateNotificationSubject, TypeNotification.IsCreateNotificationSubject);
            notification = IsTrueOrFalse(notification, systemNotification.IsCommentNotification, TypeNotification.IsCommentNotification);
            notification = IsTrueOrFalse(notification, systemNotification.IsTeacherQuestionSubject, TypeNotification.IsTeacherQuestionSubject);
            notification = IsTrueOrFalse(notification, systemNotification.IscrudExam, TypeNotification.IscrudExam);
            notification = IsTrueOrFalse(notification, systemNotification.IscrudQuestion, TypeNotification.IscrudQuestion);
            notification = IsTrueOrFalse(notification, systemNotification.IscrudLesson, TypeNotification.IscrudLesson);
            notification = IsTrueOrFalse(notification, systemNotification.IscrudResource, TypeNotification.IscrudResource);
            notification = IsTrueOrFalse(notification, systemNotification.IscrudDocument, TypeNotification.IscrudDocument);
            if(isRead.HasValue)
            {
                notification = notification.Where(no => no.IsRead == isRead);
            }
            return notification.Select(no => new NotificationDTO
            {
                NotificationId = no.NotificationId,
                UserId = no.Notification!.UserId,
                Content = no.Notification!.Content,
                CreateDate = no.Notification!.CreateDate,
                IsRead = no.IsRead,
                UserName=no.Notification.applicationUser!.UserName,
                urlImage=uploadService.GetUrlImage(no.Notification.applicationUser!.Avatar,"Avatar")
            }).ToList();
        }

        public async Task ReadNotification(string userId, int notificationId)
        {
            var notification = await context.myNotifications.FirstOrDefaultAsync(no => no.UserId == userId && no.NotificationId == notificationId);
            if (notification != null)
            {
                notification.IsRead = !notification.IsRead;
                await context.SaveChangesAsync();
            }
        }
        public  IQueryable<MyNotification> IsTrueOrFalse(IQueryable<MyNotification> notification, bool isSystem, string type)
        {
            if (isSystem)
            {
                return notification;
            }
            notification = notification.Where(no => no.Notification!.TypeNotification != type);
            return notification;
        }
    }
}
