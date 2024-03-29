using DocumentFormat.OpenXml.Spreadsheet;
using Library.Data;
using Library.Model.DTO;
using Library.Repository.ExamRepository;
using Library.Repository.SystemNotificationRepository;
using Library.Services.ApproveDocumetService;
using Library.Services.JWTService;
using Library.Services.NotificationService;
using Microsoft.EntityFrameworkCore;

namespace Library.Services.ApproveExamServices
{
    public class ApproveExamService : IApporveExamServices
    {
        private readonly MyDB context;
        private readonly INotificationService notificationService;
        private readonly IJWTSevice jWTSevice;

        public ApproveExamService(MyDB context,INotificationService notificationService,IJWTSevice jWTSevice) 
        { 
            this.context=context;
            this.notificationService=notificationService;
            this.jWTSevice = jWTSevice;
        }
        public async Task Approve(int id)
        {
            var userId = await jWTSevice.ReadToken();
            var exam = await context.exams.FirstOrDefaultAsync(ex => ex.Id == id);
            if (exam != null)
            {
                if (exam.Status == status.Wait.ToString())
                {
                    exam.Status = status.Complete.ToString();
                    await context.SaveChangesAsync();
                    List<string> listUserId = new List<string>() { userId };
                    await notificationService.CreateNotification(TypeNotification.IsCancelExam,$"Bạn phê duyêt tệp đề thi có tên {exam.Name}", listUserId, userId);
                }
            }
        }

        public async Task Cancel(int id)
        {
            var userId = await jWTSevice.ReadToken();
            var exam = await context.exams.FirstOrDefaultAsync(ex => ex.Id == id);
            if (exam != null)
            {
                if (exam.Status == status.Wait.ToString())
                {
                    exam.Status = status.Cancel.ToString();
                    await context.SaveChangesAsync();
                    List<string> listUserId = new List<string>() { userId };
                    await notificationService.CreateNotification(TypeNotification.IsChangleListRole, $"Bạn đã hủy tệp đề thi có tên {exam.Name}", listUserId, userId);


                }
            }
        }
    }
}
