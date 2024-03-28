using Library.Data;
using Library.DTO;
using Library.Model;
using Library.Services.DocumentRepository;
using Library.Services.LessonReponsitory;
using Library.Services.UploadService;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Library.Services.LessonReponsitory
{
    public class LessonReponsitory:ILessonReponsitory
    {
        private readonly MyDB context;
        private readonly IDocumentReponsitory documentReponsitory;
        private readonly IUploadService uploadService;

        public LessonReponsitory(MyDB context,IDocumentReponsitory documentReponsitory,IUploadService uploadService)
        {
            this.context = context;
            this.documentReponsitory = documentReponsitory;
            this.uploadService = uploadService;
        }
        public async Task CreateLesson(LessonModel model)
        {
            var documentModel = new DocumentModel
            {
                Classify=TypeDocument.Lesson,
                SubjectId=model.SubjectId,
                File=model.File,
            };
            var documentId= await documentReponsitory.CreateDocumentLesson(documentModel);
            foreach(int id in documentId)
            {
                var lesson = new Lesson
                {
                    Title = model.Title,
                    TopicId = model.TopicId,
                    DoucumentId = id,
                };
                await context.lessons.AddAsync(lesson);
                await context.SaveChangesAsync();
                
            }
        }

        public async Task DeleteLesson(int Id)
        {
            var lesson = await context.lessons.FirstOrDefaultAsync(le => le.Id == Id);
            if (lesson != null)
            {
                context.lessons.Remove(lesson);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<LessonDTO>> GetAllLesson(int TopicId, int? ClassId)
        {
            if(ClassId.HasValue)
            {
                return await context.lessons.Include(f => f.Document)
                    .Where(le => le.TopicId == TopicId && le.ClassLessons!.Any(cl => cl.ClassId == ClassId))
                    .Select(le => new LessonDTO
                {
                    Id = le.Id,
                    Title = le.Title,
                    UrlDocument=uploadService.GetUrlImage(le.Document!.Name, "Document")
                }).ToListAsync();
            }
            return await context.lessons.Where(le => le.TopicId == TopicId).Select(le => new LessonDTO
            {
                Id = le.Id,
                Title = le.Title,
                UrlDocument = uploadService.GetUrlImage( le.Document!.Name,"Document")
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
