using Library.DTO;
using Library.Model;

namespace Library.Services.QuestionRepository
{
    public interface IQuestionSubjectRepository
    {
        Task CreateQuestionSubject(QuestionSubejctModel model);
        Task<List<QuestionSubjectDTO>> GetAllQuestionSubject(int subjectId,string? search,int? classRoomId,int?lessonId,string? OrderBy,string?FilterQuestion);
        Task LikeQuestion(int Id);
        Task<QuestionSubjectDTO> GetQuestionSubject(int Id);
    }
}
