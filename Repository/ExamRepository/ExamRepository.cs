﻿using DocumentFormat.OpenXml.Spreadsheet;
using Library.Data;
using Library.Model;
using Library.Model.DTO;
using Library.Repository.MultipleChoiceRepository;
using Library.Repository.RoleReponsitory;
using Library.Repository.UserReponsitory;
using Library.Services.ExcelService;
using Library.Services.JWTService;
using Library.Services.NotificationService;
using Library.Services.UploadService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository.ExamRepository
{
    public class ExamRepository : IExamRepository
    {
        private readonly MyDB context;
        private readonly IMultipleChoiceRepository multipleChoiceRepository;
        private readonly IExcelService excelService;
        private readonly IUploadService uploadService;
        private readonly IJWTSevice jWTSevice;
        private readonly INotificationService notificationService;
        private readonly RoleManager<Role> roleManager;
        private readonly IUserReponsitory userReponsitory;

        public ExamRepository(MyDB context, IMultipleChoiceRepository multipleChoiceRepository, IExcelService excelService, IUploadService uploadService, IJWTSevice jWTSevice,
             INotificationService notificationService,RoleManager<Role> roleManager ,IUserReponsitory userReponsitory)
        {
            this.context = context;
            this.multipleChoiceRepository = multipleChoiceRepository;
            this.excelService = excelService;
            this.uploadService = uploadService;
            this.jWTSevice = jWTSevice;
            this.notificationService = notificationService;
            this.roleManager = roleManager;
            this.userReponsitory = userReponsitory;
        }
        public async Task CreateExamMultipleChoice(ExamMupliteChoiceModel model)
        {
            var userId = await jWTSevice.ReadToken();
            var exam = new Exam
            {
                Form = "Trắc nghiệm",
                Name = model.Name + ".xlsx",
                Time = model.Time,
                Create_At = DateTime.Now,
                UserId = userId,
                Subjectid = model.SubjectId,
                Type = ".xlsx"

            };
            await context.exams.AddAsync(exam);
            await context.SaveChangesAsync();
            List<int> listQuestionId = new List<int>();
            foreach (var questions in model.Question!)
            {
                var questionModel = new QuestionModel
                {
                    Context = questions.Context,
                    Level = questions.Level,
                    SubjectId = questions.SubjectId,
                    answers = questions.answers,


                };
                var questionid = await multipleChoiceRepository.CreateQuestion(questionModel);
                listQuestionId.Add(questionid);
                var questionExam = new QuestionExam
                {
                    QuestionId = questionid,
                    Examid = exam.Id
                };
                await context.questionExams.AddAsync(questionExam);
                await context.SaveChangesAsync();
            }
            await excelService.CreateExcel(exam.Name, listQuestionId);
            var user = await context.Users.FirstOrDefaultAsync(us => us.Id == userId);
            var role = await roleManager.FindByNameAsync(AppRole.Leader);
            var userAdmin = await userReponsitory.GetAllUser(null, role.Id);

            List<string> listUserId = new List<string> { userId };
            await notificationService.CreateNotification(TypeNotification.IscrudExam,$"{user!.UserName} thêm bài thi thành công", listUserId, userId);
            await notificationService.CreateNotification(TypeNotification.IsSaveExam, $"{user} thêm bài thi thành công", userAdmin.Select(x=>x.Id).ToList(), userId);
        }

        public async Task<List<ExamDTO>> GetAllExam(int? subjectid, string? teacherId, status? status)
        {

            var exam = context.exams.Include(f => f.applicationUsers).AsQueryable();
            if (subjectid.HasValue)
            {
                exam = exam.Where(x => x.Subjectid == subjectid);
            }
            if (!string.IsNullOrEmpty(teacherId))
            {
                exam = exam.Where(ex => ex.UserId == teacherId);
            }
            if (status.HasValue)
            {
                exam = exam.Where(ex => ex.Status == status.Value.ToString());
            }
            return await exam.Select(x => new ExamDTO
            {
                Id = x.Id,
                Name = x.Name,
                Form = x.Form,
                Time = x.Time,
                UserName = x.applicationUsers!.UserName,
                Create_at = x.Create_At,
                Status = x.Status,
            }).ToListAsync();
        }
        public async Task<ExamDetailDTO> getExam(int Id)
        {
            var exam = await context.exams.Include(f => f.QuestionExams)!
                .ThenInclude(x => x.Question)
                .ThenInclude(f => f!.Answers)
                .Include(f => f.Subject)
                .FirstOrDefaultAsync(ex => ex.Id == Id);

            if (exam == null)
            {
                return null;
            }
            return new ExamDetailDTO
            {
                Name = exam.Name,
                Form = exam.Form,
                Time = exam.Time,
                Status = exam.Status,
                NameSubject = exam.Subject!.Name,
                questionDetails = exam.QuestionExams == null ? null : exam.QuestionExams.Select(x => new QuestionDetail
                {
                    context = x.Question!.Context,
                    Answers = x.Question.Answers!.Select(x => new AnswerDTO
                    {
                        answer = x.context,
                        CorrectAnswer = x.Istrue,
                    }).ToList()
                }).ToList()

            };
        }
        public async Task CreateExamEssay(ExamEssayModel model)
        {
            var userId = await jWTSevice.ReadToken();
            var exam = new Exam
            {
                Form = "Tự luận",
                Name = model.Name + ".docx",
                Time = model.Time,
                Create_At = DateTime.Now,
                UserId = userId,
                Subjectid = model.SubjectId,
                Type = ".docx"

            };
            await context.exams.AddAsync(exam);
            await context.SaveChangesAsync();
            List<int> questionId = new List<int>();
            foreach (var question in model.Context!)
            {
                var examEssay = new EssayExam
                {
                    ExamId = exam.Id,
                    Context = question.context,
                };
                await context.essayExams.AddAsync(examEssay);
                await context.SaveChangesAsync();
                questionId.Add(examEssay.Id);

            }
            await excelService.CreateWord(exam.Name, questionId);
            var user = await context.Users.FirstOrDefaultAsync(us => us.Id == userId);
            List<string> listUserId = new List<string> { userId };
            await notificationService.CreateNotification(TypeNotification.IscrudExam,$"{user!.UserName} thêm bài thi thành công", listUserId, userId);
            var role = await roleManager.FindByNameAsync(AppRole.Leader);
            var userAdmin = await userReponsitory.GetAllUser(null, role.Id);
            await notificationService.CreateNotification(TypeNotification.IsSaveExam, $"{user} thêm bài thi thành công", userAdmin.Select(x => x.Id).ToList(), userId);
        }
        public async Task<ExamEssayDTO> getExamEssay(int Id)
        {
            var exam = await context.exams.Include(f => f.essayExams)
                .Include(f => f.Subject)
                .FirstOrDefaultAsync(ex => ex.Id == Id);
            if (exam == null)
            {
                return null;
            }
            return new ExamEssayDTO
            {
                name = exam.Name,
                NameSubject = exam.Subject!.Name,
                Time = exam.Time,
                Form = exam.Form,
                ExamEssayDetail = exam.essayExams!.Select(x => new ExamEssayDetailDTO
                {
                    Context = x.Context,
                }).ToList()
            };
        }

        public async Task RandomExam(RanDomExamModel model)
        {
            Random random = new Random();
            var userId = await jWTSevice.ReadToken();
            for (int i = 1; i <= model.QuatityExam; i++)
            {
                var exam = new Exam
                {
                    Form = "Trắc nghiệm",
                    Name = model.Name,
                    Subjectid = model.SubjectId,
                    Time = "45",
                    UserId = userId,
                    Create_At = DateTime.Now.Date,
                };
                await context.exams.AddAsync(exam);
                await context.SaveChangesAsync();
                var questionEasy = await context.questions.Where(qu => qu.Level == "Thấp" && qu.SubjectId == model.SubjectId).ToListAsync();
                var questionMedium = await context.questions.Where(qu => qu.Level == "Trung bình" && qu.SubjectId == model.SubjectId).ToListAsync();
                var questionHight = await context.questions.Where(qu => qu.Level == "Cao" && qu.SubjectId == model.SubjectId).ToListAsync();
                for (int j = 1; j <= model.QuantityQuestion; j++)
                {

                    if (j <= model.QuantityEasy)
                    {

                        var ran = random.Next(0, questionEasy.Count);
                        var questionExamEasy = new QuestionExam
                        {
                            Examid = exam.Id,
                            QuestionId = questionEasy[ran].Id
                        };
                        questionEasy.RemoveAt(ran);
                        await context.questionExams.AddAsync(questionExamEasy);
                        await context.SaveChangesAsync();
                    }
                    else if (j <= model.QuantityMedium + model.QuantityEasy)
                    {
                        var ran = random.Next(0, questionMedium.Count);
                        var questionExamEasy = new QuestionExam
                        {
                            Examid = exam.Id,
                            QuestionId = questionMedium[ran].Id
                        };
                        questionMedium.RemoveAt(ran);
                        await context.questionExams.AddAsync(questionExamEasy);
                        await context.SaveChangesAsync();
                    }
                    else
                    {
                        var ran = random.Next(0, questionHight.Count);
                        var questionExamEasy = new QuestionExam
                        {
                            Examid = exam.Id,
                            QuestionId = questionHight[ran].Id
                        };
                        questionHight.RemoveAt(ran);
                        await context.questionExams.AddAsync(questionExamEasy);
                        await context.SaveChangesAsync();
                    }


                }
                
                var user = await context.Users.FirstOrDefaultAsync(us => us.Id == userId);
                List<string> listUserId = new List<string> { userId };
                await notificationService.CreateNotification(TypeNotification.IscrudExam, $"{user!.UserName} thêm bài thi thành công", listUserId, userId);
            }
        }

        public async Task UploadExam(ExamModel model)
        {
            var userId = await jWTSevice.ReadToken();
            var exam = new Exam
            {
                Time = "45 phút",
                Create_At = DateTime.Now,
                UserId = userId,
                Subjectid = model.SubjectId,
                Name = model.File!.FileName,
            };
            await context.exams.AddAsync(exam);
            await context.SaveChangesAsync();
            string nameExam = await uploadService.UploadImage("Exam", model.File!);
            string typeFile = uploadService.GetExtensionFile("Exam", nameExam);
            exam.Type = typeFile;

            if (!string.IsNullOrEmpty(model.Name))
            {
                string newName = model.Name + $"{typeFile}";
                uploadService.RenameImage("Exam", exam.Name, newName);
                exam.Name = newName;

            }
            await context.SaveChangesAsync();
            string filePath = uploadService.GetFilePath("Exam", exam.Name);
            if (typeFile == ".xlsx")
            {
                exam.Form = "Trắc nghiệm";
                var listQuestionId = await excelService.GetExcel(model.SubjectId, userId, filePath);
                foreach (var questionId in listQuestionId)
                {
                    var questionExam = new QuestionExam
                    {
                        Examid = exam.Id,
                        QuestionId = questionId,
                    };
                    await context.questionExams.AddAsync(questionExam);
                    await context.SaveChangesAsync();
                }

            }
            else
            {
                exam.Form = "Tự luận";
                await excelService.GetWord(exam.Id, filePath);
            }
            await context.SaveChangesAsync();
            var user = await context.Users.FirstOrDefaultAsync(us => us.Id == userId);
            List<string> listUserId = new List<string> { userId };
            await notificationService.CreateNotification(TypeNotification.IscrudExam, $"{user!.UserName} thêm bài thi thành công", listUserId, userId);
            var role = await roleManager.FindByNameAsync(AppRole.Leader);
            var userAdmin = await userReponsitory.GetAllUser(null, role.Id);
            await notificationService.CreateNotification(TypeNotification.IsSaveExam, $"{user} thêm bài thi thành công", userAdmin.Select(x => x.Id).ToList(), userId);
        }

        public async Task DeleteExam(int id)
        {
            var userId = await jWTSevice.ReadToken();
            var exam = await context.exams.FirstOrDefaultAsync(ex => ex.Id == id);
            if (exam != null)
            {
                if (exam.Status == StatusExam.Cancel.ToString())
                {
                    context.Remove(exam);
                    await context.SaveChangesAsync();
                    var user = await context.Users.FirstOrDefaultAsync(us => us.Id == userId);
                    List<string> listUserId = new List<string> { userId };
                    await notificationService.CreateNotification(TypeNotification.IscrudExam,$"{user!.UserName} xóa bài thi thành công", listUserId, userId);


                }
            }
        }

        public async Task RenameFile(int id, string name)
        {
            var userId = await jWTSevice.ReadToken();
            var exam = await context.exams.FirstOrDefaultAsync(ex => ex.Id == id);
            if (exam != null)
            {
                string newName = $"{name}{exam.Type}";
                uploadService.RenameImage("Exam", exam.Name, newName);
                exam.Name = newName;
                await context.SaveChangesAsync();
                var user = await context.Users.FirstOrDefaultAsync(us => us.Id == userId);
                List<string> listUserId = new List<string> { userId };
                await notificationService.CreateNotification(TypeNotification.IscrudExam,$"{user!.UserName} đổi tên bài thi thành công", listUserId, userId);
            }
        }
    }
}
