using Library.DTO;
using Library.Model;
using Library.Data;
using Microsoft.EntityFrameworkCore;

namespace Library.Services.ResourceReponsitory
{
    public class ResourceReponsitory : IResourceReponsitory
    {
        private readonly MyDB context;

        public ResourceReponsitory(MyDB context) 
        {
            this.context = context;
        
        }
        public async Task AddResourceDocument(AddDocumnetResourceModel model)
        {
            
            
            await context.SaveChangesAsync();
        }

        public async Task CreateResource(ResourceModel model)
        {
            var document = new Document
            {
                CreateUserId = model.CreateUserId,
                Classify = TypeDocument.Resource,
                Create_at = DateTime.Now,
                SubjectId = model.SubjectId,
                Name=model.File!.FileName
            };
            await context.documents.AddAsync(document);
            await context.SaveChangesAsync();
            var resource = new Resources
            {
                LessonId = model.LessonId,
                DoucmentId=document.Id

            };
            await context.resources.AddAsync(resource);
            await context.SaveChangesAsync();
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Upload", model.File!.FileName);
            using (var streamFile = new FileStream(filePath, FileMode.Create))
            {
                model.File.CopyTo(streamFile);
            }

        }

        public async Task<List<DTOResource>> GetAllResourceLesson(int LessonId)
        {
            var resource = await context.resources.Where(re => re.LessonId == LessonId).ToListAsync();
            return resource.Select(re => new DTOResource
            {
                Id=re.Id,
                DocumentId=re.DoucmentId,
                LessonId=re.LessonId
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
                    DocumentId = resource.DoucmentId,
                    LessonId = resource.LessonId
                };
            }
            return null;
        }
    }
}
