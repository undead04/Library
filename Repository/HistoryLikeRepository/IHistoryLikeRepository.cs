using Library.Data;
using Library.Model;

namespace Library.Repository.HistoryLikeRepository
{
    public interface IHistoryLikeRepository
    {
        Task<int> CreateHistoryLike(HistoryLikeModel model);
        Task<List<HistoryLike>> ListHistoryLike(int questionSubjectId);
        Task DeleteHistoryLike(int questionSubjectId, string userId);
        Task<HistoryLike> GetHistoryLike(int questionSubjectId, string userId);
    }
}
