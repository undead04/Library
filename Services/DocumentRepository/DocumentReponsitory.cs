using Library.Data;
using Library.DTO;
using Library.Model;
using Library.Services.UploadService;
using Microsoft.EntityFrameworkCore;

namespace Library.Services.DocumentRepository
{
    public class DocumentReponsitory : IDocumentReponsitory
    {
        private readonly MyDB context;

        public  IUploadService uploadService { get; set; }

        public DocumentReponsitory(MyDB context,IUploadService uploadService)
        {
            this.context = context;
            this.uploadService=uploadService;
        }
        public async Task CreateDocumentLesson(DocumentModel model)
        {
            foreach(var file in model.File)
            {
                var document = new Document
                {
                    Create_at = DateTime.Now,
                    Classify = model.Classify,
                    Name =file.FileName,
                    SubjectId = model.SubjectId,
                    CreateUserId=model.UserId
                };
                await context.documents.AddAsync(document);
                await context.SaveChangesAsync();
                await uploadService.UploadImage(document.Id, "Document", file);
                
            }
            
           
        }

        public async Task<List<DocumentDTO>> GetAllDocumentSubject(int SubjectId)
        {

                return await context.documents
                .Include(f=>f.subject)
                .Include(f=>f.ApplicationUser)
                .Where(doc=>doc.SubjectId==SubjectId)
                .Select(doc => new DocumentDTO
                {
                    Id = doc.Id,
                    Name = doc.Name,
                    Classify = doc.Classify,
                    Create_at = doc.Create_at,
                    UserId=doc.CreateUserId,
                    UserName=doc.ApplicationUser!.UserName,
                    SubjectName=doc.subject!.Name,
                    UrlFile=uploadService.GetUrlImage(doc.Name,"Document"),
                    SubjectId=doc.SubjectId,

                }).ToListAsync();
           
        }

        
        public async Task DeleteDoucment(int documentId)
        {
            var document = await context.documents.FirstOrDefaultAsync(doc => doc.Id == documentId);
            if (document != null)
            {
                uploadService.DeleteImage("Document", document.Name);
                context.Remove(document);
                await context.SaveChangesAsync();


            }
        }
        public async Task<DocumentDTO> GetByIdDocument(int documentId)
        {
            var document = await context.documents
                .Include(f=>f.ApplicationUser)
                .Include((f=>f.subject))
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
                SubjectId=document.SubjectId,
                UserName = document.ApplicationUser!.UserName,
                SubjectName = document.subject!.Name,
                UrlFile = uploadService.GetUrlImage(document.Name, "Document"),
                

            };
        }

        public async Task AddDocumentLesson(AddDocumentLessonModel model)
        {
            var lesson = new Lesson
            {
                Title=model.Title,
                TopicId=model.TopicId,
                DoucumentId=model.DocumentId,

            };
            await context.lessons.AddAsync(lesson);
            await context.SaveChangesAsync();
        }

        public async Task<List<DocumentDTO>> GellAllDocument(string typeDocument,string UserId)
        {
            var documnet = await context.documents
                .Include(f=>f.ApplicationUser)
                .Include(f=>f.subject)
                .Where(doc=>doc.CreateUserId==UserId)
                .Where(doc => doc.Classify == typeDocument).ToListAsync();
            return documnet.Select(x => new DocumentDTO
            {
                Id=x.Id,
                Name = x.Name,
                Classify = x.Classify,
                Create_at=x.Create_at,
                StatusDocument=x.Status.ToString(),
                SubjectId=x.SubjectId,
                SubjectName=x.subject!.Name,
                UserName=x.ApplicationUser!.UserName,
                UrlFile=uploadService.GetUrlImage(x.Name,"Document")
               
            }).ToList();
        }

   
        public async Task AddDocumentResource(AddDocumnetResourceModel model)
        {
            foreach(var documentId in model.DocumnetId)
            {
                var resource = new Resources
                {
                    DoucmentId=documentId,
                    LessonId=model.LessonId,
                };
                await context.resources.AddAsync(resource);
                await context.SaveChangesAsync();
            }
        }

        public async Task RenameDocument(int Id,string name)
        {
            var document = await context.documents.FirstOrDefaultAsync(doc => doc.Id == Id);
            if(document!=null)
            {
                uploadService.RenameImage("Document", document.Name, name);
                document.Name = name;
                document.Create_at = DateTime.Now.Date;
                await context.SaveChangesAsync();
               
            }
        }
        
    }
}
