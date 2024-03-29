using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Library.Data;
using Library.Model;
using Library.Model.DTO;
using Library.Repository.ClassLessonRepository;
using Library.Repository.SystemNotificationRepository;
using Library.Services.JWTService;
using Library.Services.NotificationService;
using Library.Services.UploadService;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Library.Repository.DocumentRepository
{
    public class DocumentReponsitory : IDocumentReponsitory
    {
        private readonly MyDB context;

        public IUploadService uploadService { get; set; }

        private readonly IClassLessonRepository classLessonRepository;
        private readonly IJWTSevice jwtService;
        private readonly INotificationService notificationServer;

        public DocumentReponsitory(MyDB context, IUploadService uploadService, IClassLessonRepository classLessonRepository, IJWTSevice jWTSevice, INotificationService notificationService)
        {
            this.context = context;
            this.uploadService = uploadService;
            this.classLessonRepository = classLessonRepository;
            jwtService = jWTSevice;
            notificationServer = notificationService;
        }
        public async Task<List<int>> CreateDocumentLesson(DocumentModel model)
        {
            var documnetId = new List<int>();
            var userId = await jwtService.ReadToken();
            var user = await context.Users.FirstOrDefaultAsync(us => us.Id == userId);
            foreach (var file in model.File!)
            {
                var document = new Data.Document
                {
                    Create_at = DateTime.Now,
                    Classify = model.Classify,
                    SubjectId = model.SubjectId,
                    CreateUserId = userId,
                };
                await context.documents.AddAsync(document);
                await context.SaveChangesAsync();
                var fileName = await uploadService.UploadImage("Document", file);
                document.Name = fileName;
                await context.SaveChangesAsync();
                document.Type = uploadService.GetExtensionFile("Document", document.Name);
                document.Size = uploadService.GetSizeFile("Document", document.Name);
                await context.SaveChangesAsync();
                documnetId.Add(document.Id);
                List<string> listUserId = new List<string> { userId };
                await notificationServer.CreateNotification(TypeNotification.IscrudDocument, $"Đã thêm bài liệu {document.Name}", listUserId, userId);
            }


            return documnetId;


        }




        public async Task DeleteDoucment(int documentId)
        {
            var userId = await jwtService.ReadToken();
            var document = await context.documents.FirstOrDefaultAsync(doc => doc.Id == documentId);
            if (document != null)
            {
                uploadService.DeleteImage("Document", document.Name);
                context.Remove(document);
                var user = await context.Users.FirstOrDefaultAsync(us => us.Id == userId);
                List<string> listUserId = new List<string> { userId };
                await notificationServer.CreateNotification(TypeNotification.IscrudDocument, $"Đã xóa  bài liệu {document.Name}", listUserId, userId);
                await context.SaveChangesAsync();
            }
        }
        public async Task<DocumentDTO> GetByIdDocument(int documentId)
        {
            var document = await context.documents
                .Include(f => f.ApplicationUser)
                .Include(f => f.subject)
                .FirstOrDefaultAsync(document => document.Id == documentId);
            if (document == null)
            {
                return null;
            }
            return new DocumentDTO
            {
                Id = document.Id,
                Name = document.Name,
                Classify = document.Classify,
                Create_at = document.Create_at,

                UserName = document.ApplicationUser!.UserName,
                SubjectName = document.subject!.Name,
                UrlFile = uploadService.GetUrlImage(document.Name, "Document"),


            };
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
            var assignDocument = new AssignDocumentModel
            {
                LessonId = new int[] { lesson.Id },
                ClassId = model.classId

            };
            await classLessonRepository.AssignDocuments(assignDocument);


        }

        public async Task<List<DocumentDTO>> GellAllDocument(string? typeDocument, string? UserId, int? SubjectId, StatusDocument? statusDocument)
        {
            var document = context.documents.AsQueryable();

            if (SubjectId.HasValue)
            {
                document = document.Where(doc => doc.SubjectId == SubjectId);

            }
            if (statusDocument.HasValue)
            {
                document = document.Where(doc => doc.Status == statusDocument);
            }
            if (!string.IsNullOrEmpty(typeDocument))
            {
                document = document.Where(doc => doc.Classify == typeDocument);
            }
            if (!string.IsNullOrEmpty(UserId))
            {
                document = document.Where(doc => doc.Equals(UserId));
            }
            return await document.Select(x => new DocumentDTO
            {
                Id = x.Id,
                Name = x.Name,
                Classify = x.Classify,
                Create_at = x.Create_at,
                StatusDocument = x.Status.ToString(),
                SubjectName = x.subject!.Name,
                UserName = x.ApplicationUser!.UserName,
                UrlFile = uploadService.GetUrlImage(x.Name, "Document"),
                Type = x.Type,
                Size = x.Size,
                Note = x.Note,
                CancelDate = x.CreateCancel,

            }).ToListAsync();



        }


        public async Task AddDocumentResource(AddDocumnetResourceModel model)
        {
            foreach (var documentId in model.DocumnetId)
            {
                var resource = new Resources
                {
                    DoucmentId = documentId,
                    LessonId = model.LessonId,
                };
                foreach (var classid in model.ClassId)
                {
                    var classResource = new ClassResource
                    {
                        ClassId = classid,
                        ResourceId = resource.Id
                    };
                    await context.classResources.AddAsync(classResource);
                    await context.SaveChangesAsync();
                }
                await context.resources.AddAsync(resource);
                await context.SaveChangesAsync();
            }
        }

        public async Task RenameDocument(int Id, string name)
        {
            var document = await context.documents.FirstOrDefaultAsync(doc => doc.Id == Id);
            if (document != null)
            {
                string newName = $"{name}{document.Type}";
                uploadService.RenameImage("Document", document.Name, newName);
                document.Name = newName;
                document.Create_at = DateTime.Now.Date;
                await context.SaveChangesAsync();

            }
        }

    }
}
