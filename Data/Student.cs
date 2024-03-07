namespace Library.Data
{
    public class Student
    {
        public int Id { get; set; }
        public int ClassRoomId { get; set; }
        public int MajorId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public virtual ClassRoom? classRoom { get; set; }
        public virtual ApplicationUser? user { get; set; }
        public virtual Major? major { get; set; }
    }
}
