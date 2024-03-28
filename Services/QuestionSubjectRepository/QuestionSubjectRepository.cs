using Library.Data;
using Library.DTO;
using Library.Model;
using Library.Services.JWTService;
using Microsoft.EntityFrameworkCore;

namespace Library.Services.QuestionRepository
{
    public class QuestionSubjectRepository : IQuestionSubjectRepository
    {
        private readonly MyDB context;
        private readonly IJWTSevice jWTSevice;

        public QuestionSubjectRepository(MyDB context,IJWTSevice jWTSevice) 
        { 
            this.context=context;
            this.jWTSevice = jWTSevice;
        }
        public async Task CreateQuestionSubject(QuestionSubejctModel model)
        {
            var userId = await jWTSevice.ReadToken();
            var questionSubject = new QuestionSubject
            {
                UserId = userId,
                LessonId=model.LessonId,
                Title= model.Title,
                Context=model.context,
                Create_At=DateTime.Now.Date,
            };
            await context.questionSubjects.AddAsync(questionSubject);
            await context.SaveChangesAsync();
            foreach(int classId in model.ClassRoomId)
            {
                var questionClass = new QuestionClassRoom
                {
                    QuestionId=questionSubject.Id,
                    ClassRoomId=classId,
                };
                await context.questionClassRooms.AddAsync(questionClass);
                await context.SaveChangesAsync();
            }
             
        }

        public async Task<List<QuestionSubjectDTO>> GetAllQuestionSubject(int subjectId,string? search, int? classRoomId, int? lessonId, string? OrderBy, string? FilterQuestion)
        {
            var question = context.questionSubjects.Where(qu=>qu.Lesson!.topic!.SubjectId==subjectId).AsQueryable();
            if(!String.IsNullOrEmpty(search))
            {
                question = question
                    .Where(qu => qu.Context.Contains(search) || qu.Title.Contains(search) || qu.ApplicationUser!.UserName.Contains(search));

            }
            if (classRoomId.HasValue)
            {
                question=question.Where(qu=>qu.questionClassRooms!.Any(cl=>cl.ClassRoomId==classRoomId));
            }
            if (lessonId.HasValue)
            {
                question = question.Where(qu => qu.LessonId == lessonId);
            }
            question = question.OrderByDescending(qu => qu.Create_At);
            if(!String.IsNullOrEmpty(OrderBy))
            {
                switch(OrderBy)
                {
                    case "MostRecentQuestion":
                        question = question.OrderByDescending(qu => qu.Create_At);
                        break;
                    case "UnansweredQuestion":
                        question = question.Where(qu => qu.replyQuestions == null);
                        break;
                    case "QuestionAnswered": question=question.Where(question => question.replyQuestions != null);
                        break;
                }
               
            }
            return await question.Select(qu => new QuestionSubjectDTO
            {
                Id=qu.Id,
                Create_at=qu.Create_At,
                Title=qu.Title,
                context=qu.Context,
                UserId=qu.UserId,
                UserName=qu.ApplicationUser!.UserName,
               
            }).ToListAsync();
           
        }
        public async Task LikeQuestion(int Id)
        {
            var question = await context.questionSubjects.Where(qu => qu.Id == Id).FirstOrDefaultAsync();
            if(question != null)
            {
               
                await context.SaveChangesAsync();
            }
        }
        public async Task<QuestionSubjectDTO> GetQuestionSubject(int Id)
        {
            var question = await context.questionSubjects.Include(f=>f.ApplicationUser).Where(qu => qu.Id == Id).FirstOrDefaultAsync();
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
