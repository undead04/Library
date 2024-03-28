using Library.Model;

namespace Library.DTO
{
    public class RoleDetailDTO
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Create_at { get; set; }
        public ClaimSubject? claimSubject { get; set; }
        public ClaimPrivateFile? claimPrivateFile { get; set; }
        public ClaimDocument? claimDocument { get; set; }
        public ClaimExam? claimExam { get; set; }
        public ClaimNotification? claimNotification { get; set; }
        public ClaimRole? claimRole { get; set; }
        public ClaimAccount? claimAccount { get; set; }

    }
}
