namespace Library.DTO
{
    public class DocumentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Classify { get; set; }=string.Empty;
        public DateTime Create_at { get; set; }
        public string Note { get; set;} = string.Empty;
        public string StatusDocument { get; set; } = string.Empty;
        public int SubjectId { get; set;}
    }
}
