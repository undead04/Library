using Library.Model;
using Library.Model.DTO;

namespace Library.Repository.ReplyQuestionRepository
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
