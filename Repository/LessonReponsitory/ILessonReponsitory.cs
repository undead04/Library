using Library.Model;
using Library.Model.DTO;

namespace Library.Repository.LessonReponsitory
{
    public interface ILessonReponsitory
    {
        Task<int> CreateLesson(LessonModel model);
        Task AddDocumentLesson(AddDocumentLessonModel model);
        Task DeleteLesson(int Id);
        Task UpdateLesson(int Id, LessonModel model);
        Task<LessonDTO> GetLessonById(int Id);
        Task<List<LessonDTO>> GetAllLesson(int TopicId, int? ClassId);


    }
}
