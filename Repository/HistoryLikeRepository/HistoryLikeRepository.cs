using DocumentFormat.OpenXml.Spreadsheet;
using Library.Data;
using Library.Model;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository.HistoryLikeRepository
{
    public class HistoryLikeRepository : IHistoryLikeRepository
    {
        private readonly MyDB context;

        public HistoryLikeRepository(MyDB context)
        {
            this.context = context;
        }
        public async Task<int> CreateHistoryLike(HistoryLikeModel model)
        {
            var historyLike = new HistoryLike
            {
                UserId = model.UserId,
                SubjectQuestionId = model.SubjectQuestionId,
            };
            await context.historyLikes.AddAsync(historyLike);
            await context.SaveChangesAsync();
            return historyLike.SubjectQuestionId;
        }

        public async Task DeleteHistoryLike(int questionSubjectId, string userId)
        {
            var history = await context.historyLikes.FirstOrDefaultAsync(hi => hi.SubjectQuestionId == questionSubjectId && hi.UserId == userId);
            if (history != null)
            {
                context.Remove(history);
                await context.SaveChangesAsync();
            }
        }

        public async Task<HistoryLike> GetHistoryLike(int questionSubjectId, string userId)
        {
            var history = await context.historyLikes.FirstOrDefaultAsync(hi => hi.SubjectQuestionId == questionSubjectId && hi.UserId == userId);
            if (history != null)
            {
                return history;
            }
            return null;
        }

        public async Task<List<HistoryLike>> ListHistoryLike(int questionSubjectId)
        {
            var history = await context.historyLikes.Where(hi => hi.SubjectQuestionId == questionSubjectId).ToListAsync();
            return history;
        }
    }
}
