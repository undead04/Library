namespace Library.Data
{
    public class Topic
    {
        public int Id { get; set; }
        public string Title { get; set; }=string.Empty;
        public int SubjectId { get; set; }
        public ICollection<Lesson>? lessons { get; set; }
        public virtual Subject? Subject { get; set; }
    }
}
