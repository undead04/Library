using Library.Model;

namespace Library.Repository.ClassLessonRepository
{
    public interface IClassLessonRepository
    {
        Task AssignDocuments(AssignDocumentModel model);
    }
}
