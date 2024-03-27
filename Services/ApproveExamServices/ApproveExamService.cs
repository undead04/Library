using Library.Data;
using Library.Services.ApproveDocumetService;
using Library.Services.ExamRepository;
using Microsoft.EntityFrameworkCore;

namespace Library.Services.ApproveExamServices
{
    public class ApproveExamService : IApporveExamServices
    {
        private readonly MyDB context;

        public ApproveExamService(MyDB context) 
        { 
            this.context=context;
        }
        public async Task Approve(int id)
        {
            var exam = await context.exams.FirstOrDefaultAsync(ex => ex.Id == id);
            if (exam != null)
            {
                if (exam.Status == status.Wait.ToString())
                {
                    exam.Status = status.Complete.ToString();
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task Cancel(int id)
        {
            var exam = await context.exams.FirstOrDefaultAsync(ex => ex.Id == id);
            if (exam != null)
            {
                if (exam.Status == status.Wait.ToString())
                {
                    exam.Status = status.Cancel.ToString();
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
