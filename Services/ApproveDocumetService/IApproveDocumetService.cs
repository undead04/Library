using Library.Model;

namespace Library.Services.ApproveDocumetService
{
    public interface IApproveDocumetService
    {
        Task Approve(int id,string userid);
        Task Cancel(int id,ApproveCancelDocumentModel model);
    }
}
