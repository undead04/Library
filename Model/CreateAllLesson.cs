namespace Library.Model
{
    public class CreateAllLesson
    {
        public int SubjectId { get; set; }
        public int TopicId { get; set; }
        public string Title { get; set; } = string.Empty;
        public List<IFormFile>? Lesson { get; set; }
        public List<IFormFile>? Resource { get; set; }
        public int[] ClassId { get; set; } = new int[0];
    }
}
