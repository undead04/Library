namespace Library.Data
{
    public class Question
    {
        public int Id { get; set; }
        public string CreateUserId { get; set; } = string.Empty;
        public DateTime Create_at { get; set; }
        public string Context { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
    }
}
