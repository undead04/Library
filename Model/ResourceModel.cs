namespace Library.Model
{
    public class ResourceModel
    {
        public string CreateUserId { get; set; } = string.Empty;
        public int LessonId { get;set; }
        public int SubjectId { get; set; }
        public List<IFormFile>? File { get; set; }
    }
}
