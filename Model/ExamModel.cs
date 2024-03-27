namespace Library.Model
{
    public class ExamModel
    {
        public IFormFile? File { get; set; }
        public string Name { get; set; } = string.Empty;
        public int SubjectId { get; set; }
        public string UserId { get; set;} = string.Empty;
    }
}
