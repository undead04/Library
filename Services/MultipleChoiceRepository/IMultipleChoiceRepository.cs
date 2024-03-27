using Library.DTO;
using Library.Model;

namespace Library.Services.MultipleChoiceRepository
{
    public interface IMultipleChoiceRepository
    {
        Task<int> CreateQuestion(QuestionModel model);
        Task<List<QuestionDTO>> GetAllQuestion(int subjectId,string? level);
        Task DeleteQuestion(int Id);
        Task<QuestionDetail> GetQuestion(int Id);
        Task UpdateQuestion(int Id,QuestionModel model);
        
    }
}
