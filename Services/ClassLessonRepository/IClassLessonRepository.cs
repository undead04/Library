using Library.Model;

namespace Library.Services.ClassLessonRepository
{
    public interface IClassLessonRepository
    {
        Task AssignDocuments(AssignDocumentModel model);
    }
}
