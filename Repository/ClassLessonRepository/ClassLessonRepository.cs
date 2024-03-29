using Library.Data;
using Library.Model;
using Library.Model.DTO;
using Library.Services.JWTService;
using Library.Services.NotificationService;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository.ClassLessonRepository
{
    public class ClassLessonRepository : IClassLessonRepository
    {
        private readonly MyDB context;
        private readonly INotificationService notificationService;
        private readonly IJWTSevice jWTSevice;

        public ClassLessonRepository(MyDB context,INotificationService notificationService,IJWTSevice jWTSevice)
        {
            this.context = context;
            this.notificationService = notificationService;
            this.jWTSevice = jWTSevice;

        }
        public async Task AssignDocuments(AssignDocumentModel model)
        {
            var userId =await jWTSevice.ReadToken();
            List<string> listUserId = new List<string> { userId };
            foreach (int classId in model.ClassId)
            {
                var classRoom = await context.classRooms.FirstOrDefaultAsync(cl => cl.Id == classId);
                foreach (int lessonId in model.LessonId)
                {
                    var lesson = await context.lessons.FirstOrDefaultAsync(le => le.Id == lessonId);
                    var classLesson = new ClassLesson
                    {
                        ClassId = classId,
                        LessonId = lessonId,
                    };
                    await context.classLessons.AddAsync(classLesson);
                    await context.SaveChangesAsync();
                    await notificationService.CreateNotification(TypeNotification.IscrudDocument, $"{lesson!.Title} đã được thêm vào lớp ${classRoom!.Name}", listUserId,userId);
                }
            }
        }
    }
}
