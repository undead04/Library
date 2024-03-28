using Library.Data;
using Library.Model;
using Microsoft.EntityFrameworkCore;

namespace Library.Services.ApproveDocumetService
{
    public class ApproveDocumentService : IApproveDocumetService
    {
        private readonly MyDB context;

        public ApproveDocumentService(MyDB context) 
        {
            this.context = context;
        }
        public async Task Approve(int id, string userid)
        {
            var document=await context.documents.FirstOrDefaultAsync(doc=>doc.Id==id);
            if (document!=null)
            {
                if(document.Status.Equals(StatusDocument.Wait))
                {
                    document.Status = StatusDocument.Complete;
                    document.ApprovedByUserId = userid;
                    await context.SaveChangesAsync();
                }

            }
        }

        public async Task Cancel(int id,string userId,string note)
        {
            var document = await context.documents.FirstOrDefaultAsync(doc => doc.Id == id);
            if (document != null)
            {
                if (document.Status.Equals(StatusDocument.Wait))
                {
                    document.Status = StatusDocument.Cancel;
                    document.CreateCancel = DateTime.Now.Date;
                    document.ApprovedByUserId = userId;
                    document.Note = note;
                    await context.SaveChangesAsync();
                }
                

            }
        }
    }
}
