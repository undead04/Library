namespace Library.DTO
{
    public class QuestionDTO
    {
        public int Id { get; set; } 
        public string UserName { get; set; } = string.Empty;
        public DateTime Create_at { get; set; }
        public string Level { get; set; } = string.Empty;

    }
}
