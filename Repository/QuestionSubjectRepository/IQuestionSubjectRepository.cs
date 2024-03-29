using Library.Model;
using Library.Model.DTO;

namespace Library.Repository.QuestionSubjectRepository
{
    public interface IQuestionSubjectRepository
    {
        Task CreateQuestionSubject(QuestionSubejctModel model);
        Task<List<QuestionSubjectDTO>> GetAllQuestionSubject(int subjectId, string? search, int? classRoomId, int? lessonId, string? OrderBy, string? FilterQuestion);
        Task LikeQuestion(int Id);
        Task<QuestionSubjectDTO> GetQuestionSubject(int Id);
    }
}
