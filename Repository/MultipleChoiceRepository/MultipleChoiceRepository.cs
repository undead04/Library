using DocumentFormat.OpenXml.Spreadsheet;
using FluentValidation.Results;
using Library.Data;
using Library.Model;
using Library.Model.DTO;
using Library.Services.ExcelService;
using Library.Services.JWTService;
using Library.Services.NotificationService;
using Library.Services.UploadService;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository.MultipleChoiceRepository
{
    public class MultipleChoiceRepository : IMultipleChoiceRepository
    {
        private readonly MyDB context;
        private readonly IJWTSevice jWTSevice;
        private readonly INotificationService notificationService;

        public MultipleChoiceRepository(MyDB context, IJWTSevice jWTSevice,INotificationService notificationService)
        {
            this.context = context;
            this.jWTSevice = jWTSevice;
            this.notificationService = notificationService;


        }
        public async Task<int> CreateQuestion(QuestionModel model)
        {
            var userId = await jWTSevice.ReadToken();
            var question = new Question
            {
                CreateUserId = userId,
                Context = model.Context,
                Level = model.Level,
                SubjectId = model.SubjectId,
                Create_at = DateTime.Now.Date,
            };
            await context.questions.AddAsync(question);
            await context.SaveChangesAsync();
            foreach (var ansers in model.answers)
            {
                var answer = new Answer
                {
                    QuestionId = question.Id,
                    context = ansers.context,
                    Istrue = ansers.CorrectAnswer
                };
                await context.answers.AddAsync(answer);
                await context.SaveChangesAsync();
            }
            List<string> listUserId = new List<string> { userId };
            await notificationService.CreateNotification(TypeNotification.IscrudQuestion,$"{question.CodeQuestion} đổi tên bài thi thành công", listUserId, userId);
            return question.Id;
            
        }

        public async Task DeleteQuestion(int Id)
        {
            var userId = await jWTSevice.ReadToken();
            var question = await context.questions.FirstOrDefaultAsync(context => context.Id == Id);
            if (question != null)
            {
                var answers = await context.answers.Where(an => an.QuestionId == question.Id).ToListAsync();
                context.RemoveRange(answers);
                context.Remove(question);
                await context.SaveChangesAsync();
                List<string> listUserId = new List<string> { userId };
                await notificationService.CreateNotification(TypeNotification.IscrudQuestion, $"{question.CodeQuestion} tạo câu hỏi thành công", listUserId, userId);
            }
        }

        public async Task<List<QuestionDTO>> GetAllQuestion(int subjectId, string? level)
        {
            var question = await context.questions.Include(f => f.User).Where(qu => qu.SubjectId == subjectId).ToListAsync();
            if (!string.IsNullOrEmpty(level))
            {
                question = question.Where(qu => qu.Level == level).ToList();
            }
            return question.Select(qu => new QuestionDTO
            {
                Id = qu.Id,
                Create_at = qu.Create_at,
                UserName = qu.User!.UserName,
                Level = qu.Level,
            }).ToList();
        }

        public async Task<QuestionDetail> GetQuestion(int Id)
        {
            var question = await context.questions.Include(f => f.User)
                .Include(f => f.Answers)
                .FirstOrDefaultAsync(qu => qu.Id == Id);
            if (question != null)
            {
                return new QuestionDetail
                {
                    context = question.Context,
                    Answers = question.Answers.Select(an => new AnswerDTO
                    {
                        answer = an.context,
                        CorrectAnswer = an.Istrue,
                    }).ToList(),
                };
            }
            return null;
        }

        public async Task UpdateQuestion(int Id, QuestionModel model)
        {
            int i = 0;
            var userId = await jWTSevice.ReadToken();
            var question = await context.questions.Include(f => f.User).Include(f => f.Answers).FirstOrDefaultAsync(qu => qu.Id == Id);
            if (question != null)
            {
                question.Level = model.Level;
                question.SubjectId = model.SubjectId;
                question.Create_at = DateTime.Now.Date;

                foreach (var ansers in model.answers)
                {
                    var listanser = question.Answers!.ToList();
                    var anser = listanser[i];
                    anser.context = ansers.context;
                    anser.Istrue = ansers.CorrectAnswer;
                    i++;
                    await context.SaveChangesAsync();
                }
                List<string> listUserId = new List<string> { userId };
                await notificationService.CreateNotification(TypeNotification.IscrudQuestion, $"{question.CodeQuestion} tạo câu hỏi thành công", listUserId, userId);
                await context.SaveChangesAsync();
            }
        }


    }
}
