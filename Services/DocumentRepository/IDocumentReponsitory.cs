using Library.DTO;
using Library.Model;


namespace Library.Services.DocumentRepository
{
    public interface IDocumentReponsitory
    {
        Task CreateDocumentLesson(DocumentModel model);
        Task<List<DocumentDTO>> GetAllDocumentSubject(int SubjectId);
        
        Task DeleteDoucment(int documentId);
        Task<DocumentDTO> GetByIdDocument(int documentId);
        Task AddDocumentLesson(AddDocumentLessonModel model);
        Task<List<DocumentDTO>> GellAllDocument(string typeDocument, string UserId);
        
        Task AddDocumentResource(AddDocumnetResourceModel model);
        Task RenameDocument(int Id,string newName);

    }
}
