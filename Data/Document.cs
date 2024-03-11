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
        public int SubjectId { get; set; }
        public virtual Subject? subject { get; set; }
        public ICollection<Lesson>?lessons { get; set; }
        public ICollection<Resources>? resources { get; set; }
        
    }
}
