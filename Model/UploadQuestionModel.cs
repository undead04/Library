namespace Library.Model
{
    public class UploadQuestionModel
    {
        public IFormFile? File { get; set; }
        public int SubjectId { get; set; }
        public string CreateUserId { get; set; } = string.Empty;
    }
}
