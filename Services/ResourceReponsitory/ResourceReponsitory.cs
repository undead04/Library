using Library.DTO;
using Library.Model;
using Library.Data;
using Microsoft.EntityFrameworkCore;
using Library.Services.DocumentRepository;
using System.Reflection.Metadata;
using Library.Services.UploadService;

namespace Library.Services.ResourceReponsitory
{
    public class ResourceReponsitory : IResourceReponsitory
    {
        private readonly MyDB context;
        private readonly IDocumentReponsitory documnetReponsitory;

        
        private readonly IUploadService uploadService;

        public ResourceReponsitory(MyDB context, IDocumentReponsitory documentReponsitory, IUploadService uploadService)
        {
            this.context = context;
            this.documnetReponsitory = documentReponsitory;
            this.uploadService = uploadService;

        }
        public async Task AddResourceDocument(AddDocumnetResourceModel model)
        {
           foreach(var id in model.DocumnetId)
            {
                var resoure = new Resources
                {
                    LessonId=model.LessonId,
                    DoucmentId=id
                };
                await context.resources.AddAsync(resoure);
                await context.SaveChangesAsync();
            }
        }

        public async Task CreateResource(ResourceModel model)
        {
           
            var documentModel = new DocumentModel
            {
                Classify = TypeDocument.Resource,
                UserId = model.CreateUserId,
                SubjectId = model.SubjectId,
                File=model.File
            };
            var documentId = await documnetReponsitory.CreateDocumentLesson(documentModel);
            foreach(var id in documentId)
            {
                var resource = new Resources
                {
                    LessonId = model.LessonId,
                    DoucmentId = id

                };
                await context.resources.AddAsync(resource);
                await context.SaveChangesAsync();
            }
           

        }

        public async Task<List<DTOResource>> GetAllResourceLesson(int LessonId)
        {
            var resource = await context.resources.Include(f=>f.Document).Where(re => re.LessonId == LessonId).ToListAsync();
            return resource.Select(re => new DTOResource
            {
                Id=re.Id,
                UrlFile=uploadService.GetUrlImage(re.Document!.Name, "Document")
            }).ToList();
        }

        public async Task<DTOResource> GetResource(int id)
        {
            var resource = await context.resources.Where(re => re.Id == id).FirstOrDefaultAsync();
            if(resource!=null)
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
