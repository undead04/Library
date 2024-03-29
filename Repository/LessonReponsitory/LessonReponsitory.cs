using DocumentFormat.OpenXml.Spreadsheet;
using Library.Data;
using Library.Model;
using Library.Model.DTO;
using Library.Repository.DocumentRepository;
using Library.Repository.NotificationRepository;
using Library.Services.JWTService;
using Library.Services.NotificationService;
using Library.Services.UploadService;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository.LessonReponsitory
{
    public class LessonReponsitory : ILessonReponsitory
    {
        private readonly MyDB context;
        private readonly IDocumentReponsitory documentReponsitory;
        private readonly IUploadService uploadService;
        private readonly INotificationService notificationService;
        private readonly IJWTSevice jWTSevice;

        public LessonReponsitory(MyDB context, IDocumentReponsitory documentReponsitory, IUploadService uploadService,INotificationService notificationService,IJWTSevice jWTSevice)
        {
            this.context = context;
            this.documentReponsitory = documentReponsitory;
            this.uploadService = uploadService;
            this.notificationService = notificationService;
            this.jWTSevice = jWTSevice;
        }
        public async Task<int> CreateLesson(LessonModel model)
        {
            List<int> lessonId = new List<int>();

            var documentModel = new DocumentModel
            {
                Classify = TypeDocument.Lesson,
                SubjectId = model.SubjectId,
                File = model.File,
            };
            var documentId = await documentReponsitory.CreateDocumentLesson(documentModel);
            foreach (int id in documentId)
            {
                var lesson = new Lesson
                {
                    Title = model.Title,
                    TopicId = model.TopicId,
                    DoucumentId = id,
                };
                await context.lessons.AddAsync(lesson);
                await context.SaveChangesAsync();
                lessonId.Add(lesson.Id);
                string userId = await jWTSevice.ReadToken();
                List<string> listUserId = new List<string> { userId };
                var subject = await context.subjects.FirstOrDefaultAsync(su => su.Id == model.SubjectId);
                await notificationService.CreateNotification(TypeNotification.IscrudLesson, $"thêm bài giảng {lesson.Title} vào môn học {subject}", listUserId, userId);
            }
           
            return lessonId.First();
        }

        public async Task DeleteLesson(int Id)
        {
            string userId = await jWTSevice.ReadToken();
            var lesson = await context.lessons.Include(f=>f.topic).ThenInclude(f=>f.Subject).FirstOrDefaultAsync(le => le.Id == Id);
            if (lesson != null)
            {
                List<string> listUserId = new List<string> { userId };
                var subject = lesson.topic!.Subject!.Name;
                await notificationService.CreateNotification(TypeNotification.IscrudLesson, $"xóa bài giảng {lesson.Title}", listUserId, userId);
                context.lessons.Remove(lesson);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<LessonDTO>> GetAllLesson(int TopicId, int? ClassId)
        {
            if (ClassId.HasValue)
            {
                return await context.lessons.Include(f => f.Document)
                    .Where(le => le.TopicId == TopicId && le.ClassLessons!.Any(cl => cl.ClassId == ClassId))
                    .Select(le => new LessonDTO
                    {
                        Id = le.Id,
                        Title = le.Title,
                        UrlDocument = uploadService.GetUrlImage(le.Document!.Name, "Document")
                    }).ToListAsync();
            }
            return await context.lessons.Where(le => le.TopicId == TopicId).Select(le => new LessonDTO
            {
                Id = le.Id,
                Title = le.Title,
                UrlDocument = uploadService.GetUrlImage(le.Document!.Name, "Document")
            }).ToListAsync();
        }

        public async Task<LessonDTO> GetLessonById(int Id)
        {
            var lesson = await context.lessons.FirstOrDefaultAsync(le => le.Id == Id);
            if (lesson != null)
            {
                return new LessonDTO
                {
                    Id = lesson.Id,
                    Title = lesson.Title,
                    UrlDocument = uploadService.GetUrlImage("Document", lesson.Document!.Name)
                };
            }
            return null;
        }

        public async Task UpdateLesson(int Id, LessonModel model)
        {
            var lesson = await context.lessons.FirstOrDefaultAsync(le => le.Id == Id);
            if (lesson != null)
            {
                lesson.Title = model.Title;
                await context.SaveChangesAsync();
            }
        }
        public async Task AddDocumentLesson(AddDocumentLessonModel model)
        {
            var lesson = new Lesson
            {
                Title = model.Title,
                TopicId = model.TopicId,
                DoucumentId = model.DocumentId,
            };
            await context.lessons.AddAsync(lesson);
            await context.SaveChangesAsync();
        }
    }
}
