using Library.DTO;
using Library.Model;
namespace Library.Services.ReplyQuestionRepository
{
    public interface IReplyQuestionRepository
    {
        Task CreateReplyQuestion(ReplyModel model);
        Task<List<ReplyDTO>> GetAllReplyQuestion(int Id);
        Task<ReplyDTO> GetReplyQuestion(int Id);
        Task DeleteReplyQuestion(int Id);
        Task UpdateReplyQuestion(int Id, ReplyUpdateModel model);
    }
}
