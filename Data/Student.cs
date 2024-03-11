using System.ComponentModel.DataAnnotations;

namespace Library.Data
{
    public class Student
    {
        [Key]
        public string UserId { get; set; } = string.Empty;
        public int ClassRoomId { get; set; }
        public int MajorId { get; set; }
        
        public virtual ClassRoom? classRoom { get; set; }
        public virtual ApplicationUser? user { get; set; }
        public virtual Major? major { get; set; }
    }
}
