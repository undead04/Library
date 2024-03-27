using Library.Model;

namespace Library.Services.ApproveExamServices
{
    public interface IApporveExamServices
    {
        Task Approve(int id);
        Task Cancel(int id);
    }
}
