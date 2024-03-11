namespace Library.Model
{
    public class ResourceModel
    {
        public int LessonId { get;set; }
        public IFormFile? File { get; set; }
        public int SubjectId { get; set; }
    }
}
