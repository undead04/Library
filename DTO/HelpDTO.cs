namespace Library.DTO
{
    public class HelpDTO
    {
        public int Id { get; set; }
        public DateTime Create_at { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Context { get; set; } = string.Empty;
    }
}
