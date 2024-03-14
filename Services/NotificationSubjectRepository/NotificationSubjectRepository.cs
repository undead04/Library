using Library.Data;
using Library.DTO;
using Library.Model;
using Microsoft.EntityFrameworkCore;

namespace Library.Services.NotificationSubjectRepository
{
    public class NotificationSubjectRepository : INotificationSubjectRepository
    {
        private readonly MyDB context;

        public NotificationSubjectRepository(MyDB context) 
        {
            this.context = context;
        }
        public async Task CreatenotificationSubject(NoticationSubjectModel model)
        {
            var notificationSubject = new NotificationSubject
            {
                Title = model.Title,
                UserId=model.CreateUserId,
                Context=model.Context,
                SubjectId=model.SubjectId,
                Create_At=DateTime.Now.Date,
            };
            await context.notificationSubjects.AddAsync(notificationSubject);
            await context.SaveChangesAsync();
            foreach(int classRoomId in model.ClassRoomId)
            {
                var notificationClass = new NotificationClassRoom
                {
                    SubjectNotificationid = notificationSubject.Id,
                    ClassRoomId=classRoomId
                };
                await context.notificationClassRooms.AddAsync(notificationClass);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<NotificationSubjectDTO>> GetAllNotification(int subjectId, string? search, int? classRoomId)
        {
            var notification =context.notificationSubjects.Where(no => no.SubjectId == subjectId).AsQueryable();
            if(!String.IsNullOrEmpty(search))
            {
                notification = notification.Where(no => no.Title.Contains(search) || no.Context.Contains(search));
            }
            if(classRoomId.HasValue)
            {
                notification = notification.Where(no => no.notificationClassRooms!.Any(no => no.ClassRoomId == classRoomId));
            }
            return await notification.Select(no => new NotificationSubjectDTO
            {
                Id=no.Id,
                CreateUserId=no.UserId,
                Title=no.Title,
                Context=no.Context,
                Create_at=no.Create_At.Date,
                UserName=no.User!.UserName,
            }).ToListAsync();
        }
    }
}
