using Library.Data;
using Library.DTO;
using Library.Model;

namespace Library.Server
{
    public interface ILessonReponsitory
    {
        Task CreateLesson(LessonModel model);
        Task DeleteLesson(int Id);
        Task UpdateLesson(int Id, LessonModel model);
        Task<LessonDTO> GetLessonById(int Id);
        Task<List<LessonDTO>> GetAllLesson(int TopicId);

    }
}
