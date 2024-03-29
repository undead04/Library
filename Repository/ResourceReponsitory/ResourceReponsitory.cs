using Library.Model;
using Library.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using Library.Services.UploadService;
using Library.Model.DTO;
using Library.Repository.DocumentRepository;
using DocumentFormat.OpenXml.Spreadsheet;
using Library.Services.NotificationService;
using Library.Services.JWTService;

namespace Library.Repository.ResourceReponsitory
{
    public class ResourceReponsitory : IResourceReponsitory
    {
        private readonly MyDB context;
        private readonly IDocumentReponsitory documnetReponsitory;


        private readonly IUploadService uploadService;
        private readonly IJWTSevice jWTSevice;
        private readonly INotificationService notificationService;

        public ResourceReponsitory(MyDB context, IDocumentReponsitory documentReponsitory, IUploadService uploadService,IJWTSevice jWTSevice,INotificationService notificationService)
        {
            this.context = context;
            documnetReponsitory = documentReponsitory;
            this.uploadService = uploadService;
            this.jWTSevice = jWTSevice;
            this.notificationService = notificationService;

        }
        public async Task AddResourceDocument(AddDocumnetResourceModel model)
        {
            foreach (var id in model.DocumnetId)
            {
                var resoure = new Resources
                {
                    LessonId = model.LessonId,
                    DoucmentId = id
                };
                await context.resources.AddAsync(resoure);
                await context.SaveChangesAsync();
            }
        }

        public async Task CreateResource(ResourceModel model)
        {
            string userId = await jWTSevice.ReadToken();
            var documentModel = new DocumentModel
            {
                Classify = TypeDocument.Resource,
                SubjectId = model.SubjectId,
                File = model.File
            };
            var documentId = await documnetReponsitory.CreateDocumentLesson(documentModel);
            foreach (var id in documentId)
            {
                var resource = new Resources
                {
                    LessonId = model.LessonId,
                    DoucmentId = id

                };
                await context.resources.AddAsync(resource);
                await context.SaveChangesAsync();
                List<string> listUserId = new List<string> { userId };
                var user = await context.Users.FirstOrDefaultAsync(x => x.Id == userId);
                await notificationService.CreateNotification(TypeNotification.IscrudResource, $"{user!.UserName} thêm tài nguyên", listUserId, userId);
            }
        }

        public async Task<List<DTOResource>> GetAllResourceLesson(int LessonId)
        {
            var resource = await context.resources.Include(f => f.Document).Where(re => re.LessonId == LessonId).ToListAsync();
            return resource.Select(re => new DTOResource
            {
                Id = re.Id,
                UrlFile = uploadService.GetUrlImage(re.Document!.Name, "Document")
            }).ToList();
        }

        public async Task<DTOResource> GetResource(int id)
        {
            var resource = await context.resources.Where(re => re.Id == id).FirstOrDefaultAsync();
            if (resource != null)
            {
                return new DTOResource
                {
                    Id = resource.Id,
                    UrlFile = uploadService.GetUrlImage(resource.Document!.Name, "Document")
                };
            }
            return null;
        }
    }
}
