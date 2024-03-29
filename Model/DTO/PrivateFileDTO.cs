namespace Library.Model.DTO
{
    public class PrivateFileDTO
    {
        public int Id { get; set; }
        public string Create_at { get; set; } = string.Empty;
        public string CreateUserName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public string UrlFile { get; set; } = string.Empty;
    }
}
