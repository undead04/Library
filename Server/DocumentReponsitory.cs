using Library.Data;
using Library.DTO;
using Library.Model;

using Microsoft.EntityFrameworkCore;
using MimeMapping;

namespace Library.Server
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
            var document = new Data.Document
            {
                Create_at = DateTime.Now,
                Classify = model.Classify,
                Name = model.Name,
               
            };
            var approve = new Approve
            {
                DocumentId = document.Id,
               

            };
            await context.approves.AddAsync(approve);
            await context.documents.AddAsync(document);
            await context.SaveChangesAsync();
        }

        public async Task<List<DocumentDTO>> GetAllDocuments(int? SubjectId)
        {
            
            if(!SubjectId.HasValue)
            {
                return await context.documents.Select(doc => new DocumentDTO
                {
                    Id = doc.Id,
                    Name = doc.Name,
                    Classify = doc.Classify,
                    Create_at = doc.Create_at,
                   
                    Note = doc.Approve!.Note,
                }).ToListAsync();
            }
            var documents =await context.documents.ToListAsync();
            return documents.Select(doc => new DocumentDTO
            {
                Id=doc.Id,
                Name=doc.Name,
                Classify=doc.Classify,
                Create_at=doc.Create_at,
               
                Note = doc.Approve!.Note,

            }).ToList();
        }

        public async Task<string> GetDoucment(int documentId)
        {
            var documet = await context.documents.FirstOrDefaultAsync(doc => doc.Id == documentId);
           if(documet!=null)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Upload", documet.Name);
                if (File.Exists(filePath))
                {
                    return filePath;
                }
            }
            return null;
            
        }
       public async Task DeleteDoucment(int documentId)
        {
            var document = await context.documents.FirstOrDefaultAsync(doc => doc.Id == documentId);
            if(document!=null)
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
            var document= await context.documents.FirstOrDefaultAsync(document => document.Id == documentId);
            if(document==null)
            {
                return null;
            }
            return new DocumentDTO
            {
                Id = document.Id,
                Name = document.Name,
                Classify = document.Classify,
                Create_at = document.Create_at,
               
               
            };
        }
       
    }
}
