namespace Library.Data
{
    public class Tearcher
    {
        public int Id { get; set; }
        public int MajorId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public virtual ApplicationUser? user { get; set; }
        public virtual Major? major { get; set; }
        public ICollection<SubjectClassRoom>? subjectClassRooms { get; set; }
    }
}
