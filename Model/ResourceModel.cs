namespace Library.Model
{
    public class ResourceModel
    {
       
        public int LessonId { get;set; }
        public int SubjectId { get; set; }
        public List<IFormFile>? File { get; set; }
    }
}
