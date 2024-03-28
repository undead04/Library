using System.Runtime.InteropServices;

namespace Library.Model
{
    public class RoleModel
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ClaimSubject? claimSubject { get; set; }
        public ClaimPrivateFile? claimPrivateFile { get; set; }
        public ClaimDocument? claimDocument { get; set; }
        public ClaimExam? claimExam { get; set; }
        public ClaimNotification? claimNotification { get; set; }   
        public ClaimRole? claimRole { get; set; }
        public ClaimAccount?claimAccount { get; set; }

    }
    public class ClaimSubject
    {
        public bool IsView { get; set; }
        public bool IsEdit { get; set; }
    }
    public class ClaimPrivateFile
    {
        public bool IsCreate { get; set; }
        public bool IsView { get; set; }
        public bool IsEdit { get; set; }
        public bool IsDelete { get; set; }
        public bool IsDownload { get; set; }
    }
    public class ClaimDocument
    {
        public bool IsCreate { get; set; }
        public bool IsView { get; set; }
        public bool IsEdit { get; set; }
        public bool IsDelete { get; set; }
        public bool IsDownload { get; set; }
        public bool IsAdd { get; set; }
        
    }
    public class ClaimExam
    {
        public bool IsCreate { get; set; }
        public bool IsView { get; set; }
        public bool IsEdit { get; set; }
        public bool IsDelete { get; set; }
        public bool IsDownload { get; set; }
        public bool IsApprove { get; set; }
    }
    public class ClaimNotification
    {
        
        public bool IsView { get; set; }
        public bool IsEdit { get; set; }
        public bool IsDelete { get; set; }
        public bool IsSystem { get; set; }
    }
    public class ClaimRole
    {
        public bool IsCreate { get; set; }
        public bool IsView { get; set; }
        public bool IsEdit { get; set; }
        public bool IsDelete { get; set; }
       
    }
    public class ClaimAccount
    {
       
        public bool IsView { get; set; }
        public bool IsEdit { get; set; }
       
    }

}
