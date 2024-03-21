using Library.Data;
using Library.DTO;
using Library.Model;

namespace Library.Services.DocumentRepository
{
    public interface IDocumentReponsitory
    {
        Task<List<int>> CreateDocumentLesson(DocumentModel model);
        Task DeleteDoucment(int documentId);
        Task<DocumentDTO> GetByIdDocument(int documentId);
        Task AddDocumentLesson(AddDocumentLessonModel model);
        Task<List<DocumentDTO>> GellAllDocument(string? typeDocument, string? UserId,int?SubjectId,StatusDocument? statusDocument);
        
        Task AddDocumentResource(AddDocumnetResourceModel model);
        Task RenameDocument(int Id,string newName);

    }
}
