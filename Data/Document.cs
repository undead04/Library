using System.ComponentModel.DataAnnotations;

namespace Library.Data
{
    public enum StatusDocument
    {
        [Display(Name = "Đã hủy")]
        Cancel = 1,
        [Display(Name = "Đã phê duyệt")]
        Complete = 2,
        [Display(Name = "Chờ phê duyệt")]
        Wait = 3



    }
    public class Document
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Classify { get; set; } = string.Empty;
        public DateTime Create_at { get; set; }
        public StatusDocument Status { get; set; } = StatusDocument.Wait;
        public string CreateUserId { get; set; } = string.Empty;
        public int SubjectId { get; set; }
        public DateTime CreateCancel { get; set; }
        public string Note { get; set; }=string.Empty;
        public string ApprovedByUserId { get; set; }=string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public virtual Subject? subject { get; set; }
        public virtual ApplicationUser? ApplicationUser {get;set;}
        public ICollection<Lesson>?lessons { get; set; }
        public ICollection<Resources>? resources { get; set; }
        
    }
}
