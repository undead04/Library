using System.ComponentModel.DataAnnotations;

namespace Library.Data
{
    public class Tearcher
    {
        
        public int MajorId { get; set; }
        [Key]
        public string UserId { get; set; } = string.Empty;
        public virtual ApplicationUser? user { get; set; }
        public virtual Major? major { get; set; }
        public ICollection<SubjectClassRoom>? subjectClassRooms { get; set; }
    }
}
