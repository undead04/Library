namespace Library.Model
{
    public class DocumentModel
    {
        
       
        public string Classify { get; set; } = string.Empty;
        public int SubjectId { get; set; }
        public IFormFile? File { get; set; }

          
    }
}
