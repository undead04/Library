using Library.Data;
using Library.DTO;
using Library.Model;
using Microsoft.EntityFrameworkCore;

namespace Library.Server.DocumentRepository
{
    public class DocumentReponsitory : IDocumentReponsitory
    {
        private readonly MyDB context;

        public DocumentReponsitory(MyDB context)
        {
            this.context = context;
        }
        public async Task CreateDocumentLesson(DocumentModel model)
        {
            var document = new Document
            {
                Create_at = DateTime.Now,
                Classify = model.Classify,
                Name = model.File!.FileName,
                SubjectId=model.SubjectId,

            };
            await context.documents.AddAsync(document);
            await context.SaveChangesAsync();
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Upload", model.File!.Name);
            using(FileStream fileStream =new FileStream(filePath,FileMode.Create))
            {
              await model.File.CopyToAsync(fileStream);
            }
           
        }

        public async Task<List<DocumentDTO>> GetAllDocumentSubject(int? SubjectId)
        {

            if (!SubjectId.HasValue)
            {
                return await context.documents.Select(doc => new DocumentDTO
                {
                    Id = doc.Id,
                    Name = doc.Name,
                    Classify = doc.Classify,
                    Create_at = doc.Create_at,


                }).ToListAsync();
            }
            var documents = await context.documents.ToListAsync();
            return documents.Select(doc => new DocumentDTO
            {
                Id = doc.Id,
                Name = doc.Name,
                Classify = doc.Classify,
                Create_at = doc.Create_at,



            }).ToList();
        }

        
        public async Task DeleteDoucment(int documentId)
        {
            var document = await context.documents.FirstOrDefaultAsync(doc => doc.Id == documentId);
            if (document != null)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Upload", document.Name);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                context.Remove(document);
                await context.SaveChangesAsync();


            }
        }
        public async Task<DocumentDTO> GetByIdDocument(int documentId)
        {
            var document = await context.documents.FirstOrDefaultAsync(document => document.Id == documentId);
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

        public async Task<List<DocumentDTO>> GellAllDocument(string typeDocument)
        {
            var documnet = await context.documents.Where(doc => doc.Classify == typeDocument).ToListAsync();
            return documnet.Select(x => new DocumentDTO
            {
                Id=x.Id,
                Name = x.Name,
                Classify = x.Classify,
                Create_at=x.Create_at,
                StatusDocument=x.Status.ToString(),
                SubjectId=x.SubjectId,
            }).ToList();
        }
    }
}
