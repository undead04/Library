using Library.Data;
using Library.Model;
using Library.Model.DTO;
using Library.Repository.HistoryLikeRepository;
using Library.Services.JWTService;
using Library.Services.NotificationService;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository.QuestionSubjectRepository
{
    public class QuestionSubjectRepository : IQuestionSubjectRepository
    {
        private readonly MyDB context;
        private readonly IJWTSevice jWTSevice;
        private readonly IHistoryLikeRepository historyLikeRepository;
        private readonly INotificationService notificationService;
        

        public QuestionSubjectRepository(MyDB context, IJWTSevice jWTSevice, IHistoryLikeRepository historyLikeRepository,
            INotificationService notificationService)
        {
            this.context = context;
            this.jWTSevice = jWTSevice;
            this.historyLikeRepository = historyLikeRepository;
            this.notificationService = notificationService;
        }
        public async Task CreateQuestionSubject(QuestionSubejctModel model)
        {
            var userId = await jWTSevice.ReadToken();
            var questionSubject = new QuestionSubject
            {
                UserId = userId,
                LessonId = model.LessonId,
                Title = model.Title,
                Context = model.context,
                Create_At = DateTime.Now.Date,
            };
            List<string> listUserId = new List<string>();
            await context.questionSubjects.AddAsync(questionSubject);
            await context.SaveChangesAsync();
            if (model.ClassRoomId == null)
            {
               
                    var userTeacher = await context.questionSubjects.FirstOrDefaultAsync(x => x.Id == questionSubject.Id);
                    listUserId.Add(userTeacher!.UserId);
                    await notificationService.CreateNotification(TypeNotification.IsCommentMyQuestion, $"học sinh đã đặt câu hỏi trong bài giảng của bạn", listUserId, userId);
                
            }
            foreach (int classId in model.ClassRoomId!)
            {
                var questionClass = new QuestionClassRoom
                {
                    QuestionId = questionSubject.Id,
                    ClassRoomId = classId,
                };
                await context.questionClassRooms.AddAsync(questionClass);
                await context.SaveChangesAsync();
                var ClassStudent = await context.students.Where(qu => qu.ClassRoomId == classId).ToListAsync();
                var studentId = ClassStudent.Select(st => st.UserId);
                listUserId.AddRange(studentId);
                await notificationService.CreateNotification(TypeNotification.IsTeacherQuestionSubject, $"giáo viên đặt câu hỏi mới vào lớp của bạn", listUserId, userId);
            }


        }

        public async Task<List<QuestionSubjectDTO>> GetAllQuestionSubject(int subjectId, string? search, int? classRoomId, int? lessonId, string? OrderBy, string? FilterQuestion)
        {
            string userId = await jWTSevice.ReadToken();
            var question = context.questionSubjects.Where(qu => qu.Lesson!.topic!.SubjectId == subjectId).AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                question = question
                    .Where(qu => qu.Context.Contains(search) || qu.Title.Contains(search) || qu.ApplicationUser!.UserName.Contains(search));

            }
            if (classRoomId.HasValue)
            {
                question = question.Where(qu => qu.questionClassRooms!.Any(cl => cl.ClassRoomId == classRoomId));
            }
            if (lessonId.HasValue)
            {
                question = question.Where(qu => qu.LessonId == lessonId);
            }
            question = question.OrderByDescending(qu => qu.Create_At);
            if (!string.IsNullOrEmpty(OrderBy))
            {
                switch (OrderBy)
                {
                    case "MostRecentQuestion":
                        question = question.OrderByDescending(qu => qu.Create_At);
                        break;
                    case "UnansweredQuestion":
                        question = question.Where(qu => qu.replyQuestions == null);
                        break;
                    case "QuestionAnswered":
                        question = question.Where(question => question.replyQuestions != null);
                        break;
                }

            }
            if (!string.IsNullOrEmpty(FilterQuestion))
            {
                switch (FilterQuestion)
                {
                    case "myquestion":
                        question = question.Where(qu => qu.UserId == userId);
                        break;
                    case "likequestion":
                        question = question.Where(qu => qu.historyLikes!.Any(hi => hi.UserId == userId));
                        break;
                }
            }
            return await question.Select(qu => new QuestionSubjectDTO
            {
                Id = qu.Id,
                Create_at = qu.Create_At,
                Title = qu.Title,
                context = qu.Context,
                UserId = qu.UserId,
                UserName = qu.ApplicationUser!.UserName,
                Like = qu.historyLikes != null ? qu.historyLikes.Count : 0,

            }).ToListAsync();

        }
        public async Task LikeQuestion(int Id)
        {
            var userId = await jWTSevice.ReadToken();
            var listHistoryLike = await historyLikeRepository.ListHistoryLike(Id);
            if (listHistoryLike.Count > 0)
            {
                await historyLikeRepository.DeleteHistoryLike(Id, userId);
            }
            else
            {
                var model = new HistoryLikeModel
                {
                    SubjectQuestionId = Id,
                    UserId = userId,
                };
                await historyLikeRepository.CreateHistoryLike(model);
            }
            List<string> listUserId = new List<string>();
            var questionSubject = await context.questionSubjects.Where(qu => qu.Id == Id).FirstOrDefaultAsync();
            listUserId.Add(questionSubject!.UserId);
            var username = await context.Users.FirstOrDefaultAsync(us => us.Id == userId);
            await notificationService.CreateNotification(TypeNotification.IsCommentMyQuestion, $"{username} đã tương tác với câu hỏi của bạn", listUserId, userId);


        }
        public async Task<QuestionSubjectDTO> GetQuestionSubject(int Id)
        {
            var question = await context.questionSubjects.Include(f => f.ApplicationUser).Where(qu => qu.Id == Id).FirstOrDefaultAsync();
            if (question != null)
            {
                return new QuestionSubjectDTO
                {
                    Id = question.Id,
                    Create_at = question.Create_At,
                    Title = question.Title,
                    context = question.Context,
                    UserId = question.UserId,
                    UserName = question.ApplicationUser!.UserName,

                };
            }
            return null;
        }
    }
}
