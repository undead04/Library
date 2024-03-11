using System.ComponentModel.DataAnnotations;

namespace Library.Model
{
    public class LessonModel
    {
        public int SubjectId { get; set; }
        public int TopicId { get; set; }
        public string Title { get; set; } = string.Empty;
        public IFormFile? File { get;set; }
    }
}
