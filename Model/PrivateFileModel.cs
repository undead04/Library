namespace Library.Model
{
    public class PrivateFileModel
    {
        public List<IFormFile>? Files { get;set; }
        public string CreateUserId { get; set; } = string.Empty;
    }
}
