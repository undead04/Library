namespace Library.Data
{
    public class SubjectClassRoom
    {
        public int Id { get; set; }
        public int SubjectId { get; set;}
        public int ClassRoomId { get; set; }
        public string TearcherId { get; set; } = string.Empty;
        public virtual Subject? Subject { get; set; }
        public virtual ClassRoom? ClassRoom { get; set; }
        public virtual Tearcher? Tearcher { get; set; }
    }
}
