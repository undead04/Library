namespace Library.Model
{
    public class DocumentModel
    {

        public string Classify { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public int SubjectId { get; set; }
        public List<IFormFile>? File { get; set; }


    }
}
