using Library.Data;
using Library.Model;
using Library.Model.DTO;
using Library.Repository.SystemNotificationRepository;
using Library.Services.JWTService;
using Library.Services.NotificationService;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository.ReplyQuestionRepository
{
    public class ReplyQuestionRepository : IReplyQuestionRepository
    {
        private readonly MyDB context;
        private readonly IJWTSevice jWTSevice;
       
        private readonly INotificationService notificationService;

        public ReplyQuestionRepository(MyDB context, IJWTSevice jWTSevice, INotificationService notificationService)
        {
            this.context = context;
            this.jWTSevice = jWTSevice;
            this.notificationService = notificationService;
        }
        public async Task CreateReplyQuestion(ReplyModel model)
        {
            var userId = await jWTSevice.ReadToken();
            var replyQuestio = new ReplyQuestion
            {
                Create_At = DateTime.Now.Date,
                UserId = userId,
                QuestionId = model.QuestionId,
                Context = model.Context,
            };
            await context.replyQuestions.AddAsync(replyQuestio);
            await context.SaveChangesAsync();
            var questionSubject = await context.questionSubjects.FirstOrDefaultAsync(qu => qu.Id == model.QuestionId);
            var createQuestionUserId = questionSubject!.UserId;
            List<string> listUserId = new List<string> { createQuestionUserId };
            var createUserName = context.Users.FirstOrDefault(us => us.Id == userId)!.UserName;
            await notificationService.CreateNotification(TypeNotification.IsCommentMyQuestion, $"{createUserName} đã trả lời câu hỏi của bạn", listUserId, userId);
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
            var reply = await context.replyQuestions.Include(f => f.User).Where(re => re.QuestionId == Id).ToListAsync();
            return reply.Select(x => new ReplyDTO
            {
                Id = x.Id,
                UserId = x.UserId,
                Create_at = x.Create_At,
                Context = x.Context,
                UserName = x.User.UserName,
            }).ToList();
        }

        public async Task<ReplyDTO> GetReplyQuestion(int Id)
        {
            var reply = await context.replyQuestions.Include(f => f.User).FirstOrDefaultAsync(re => re.Id == Id);
            if (reply != null)
            {
                return new ReplyDTO
                {
                    Id = reply.Id,
                    UserId = reply.UserId,
                    Context = reply.Context,
                    UserName = reply.User!.UserName,
                    Create_at = reply.Create_At,
                };
            }
            return null;
        }

        public async Task UpdateReplyQuestion(int Id, ReplyUpdateModel model)
        {
            var reply = await context.replyQuestions.FirstOrDefaultAsync(re => re.Id == Id);
            if (reply != null)
            {
                reply.Context = model.Context;
                reply.Create_At = DateTime.Now.Date;
                await context.SaveChangesAsync();
            }
        }
    }
}
