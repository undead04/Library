using Library.DTO;
using Library.Model;


namespace Library.Server
{
    public interface IDocumentReponsitory
    {
        Task CreateDocumentLesson(DocumentModel model);
        Task<List<DocumentDTO>> GetAllDocuments(int? SubjectId);
        Task<string> GetDoucment(int documentId);
        Task DeleteDoucment(int documentId);
        Task<DocumentDTO> GetByIdDocument(int documentId);
        
    }
}
