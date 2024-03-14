using Library.Data;
using Library.DTO;
using Library.Model;
using Microsoft.EntityFrameworkCore;

namespace Library.Services.ReplyQuestionRepository
{
    public class ReplyQuestionRepository : IReplyQuestionRepository
    {
        private readonly MyDB context;

        public ReplyQuestionRepository(MyDB context) 
        {
            this.context = context;
        }
        public async Task CreateReplyQuestion(ReplyModel model)
        {
            var replyQuestio = new ReplyQuestion
            {
                Create_At = DateTime.Now.Date,
                UserId=model.UserId,
                QuestionId=model.QuestionId,
                Context=model.Context,
            };
            await context.replyQuestions.AddAsync(replyQuestio);
            await context.SaveChangesAsync();
        }

        public async Task DeleteReplyQuestion(int Id)
        {
            var reply = await context.replyQuestions.FirstOrDefaultAsync(re => re.Id == Id);
            if (reply != null)
            {
                context.Remove(reply);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<ReplyDTO>> GetAllReplyQuestion(int Id)
        {
            var reply = await context.replyQuestions.Include(f=>f.User).Where(re => re.QuestionId == Id).ToListAsync();
            return reply.Select(x => new ReplyDTO
            {
                Id=x.Id,
                UserId=x.UserId,
                Create_at=x.Create_At,
                Context=x.Context,
                UserName=x.User.UserName,
            }).ToList();
        }

        public async Task<ReplyDTO> GetReplyQuestion(int Id)
        {
            var reply = await context.replyQuestions.Include(f=>f.User).FirstOrDefaultAsync(re => re.Id == Id);
            if (reply != null)
            {
                return new ReplyDTO
                {
                    Id = reply.Id,
                    UserId = reply.UserId,
                    Context=reply.Context,
                    UserName=reply.User!.UserName,
                    Create_at=reply.Create_At,
                };
            }
            return null;
        }

        public async Task UpdateReplyQuestion(int Id, ReplyUpdateModel model)
        {
            var reply = await context.replyQuestions.FirstOrDefaultAsync(re => re.Id == Id);
            if (reply != null)
            {
                reply.Context=model.Context;
                reply.Create_At = DateTime.Now.Date;
                await context.SaveChangesAsync();
            }
        }
    }
}
