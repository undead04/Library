using DocumentFormat.OpenXml.VariantTypes;
using Library.Data;
using Library.Model;
using Library.Model.DTO;
using Library.Repository.SystemNotificationRepository;
using Library.Services.NotificationService;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository.NotificationSubjectRepository
{
    public class NotificationSubjectRepository : INotificationSubjectRepository
    {
        private readonly MyDB context;
       
        private readonly INotificationService notificationService;

        public NotificationSubjectRepository(MyDB context, INotificationService notificationService)
        {
            this.context = context;
            this.notificationService = notificationService;
        }
        public async Task CreatenotificationSubject(NoticationSubjectModel model, string userId)
        {
            var notificationSubject = new NotificationSubject
            {
                Title = model.Title,
                UserId = userId,
                Context = model.Context,
                SubjectId = model.SubjectId,
                Create_At = DateTime.Now.Date,
            };
            await context.notificationSubjects.AddAsync(notificationSubject);
            await context.SaveChangesAsync();
            List<string> listUserId = new List<string>();
            foreach (int classRoomId in model.ClassRoomId)
            {
                var notificationClass = new NotificationClassRoom
                {
                    SubjectNotificationid = notificationSubject.Id,
                    ClassRoomId = classRoomId
                };
                await context.notificationClassRooms.AddAsync(notificationClass);
                await context.SaveChangesAsync();
                var ClassStudent = await context.students.Where(qu => qu.ClassRoomId == classRoomId).ToListAsync();
                var studentId = ClassStudent.Select(st => st.UserId);
                listUserId.AddRange(studentId);
            }
           
             await notificationService.CreateNotification(TypeNotification.IsCreateNotificationSubject, $"giáo viên đặt thông báo mới vào lớp của bạn", listUserId, userId);
            
        }

        public async Task<List<NotificationSubjectDTO>> GetAllNotification(int subjectId, string? search, int? classRoomId)
        {
            var notification = context.notificationSubjects.Where(no => no.SubjectId == subjectId).AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                notification = notification.Where(no => no.Title.Contains(search) || no.Context.Contains(search));
            }
            if (classRoomId.HasValue)
            {
                notification = notification.Where(no => no.notificationClassRooms!.Any(no => no.ClassRoomId == classRoomId));
            }
            return await notification.Select(no => new NotificationSubjectDTO
            {
                Id = no.Id,
                CreateUserId = no.UserId,
                Title = no.Title,
                Context = no.Context,
                Create_at = no.Create_At.Date,
                UserName = no.User!.UserName,
            }).ToListAsync();
        }
    }
}
