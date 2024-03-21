using Library.Model;

namespace Library.DTO
{
    public class SubjectDTO:SubjectModel
    {
        public int Id { get; set; }
       
        public string MajorName { get; set; } = string.Empty;
        public string StatusDocument { get; set; }=string.Empty;
        public int TotalDoucment { get; set; }
        public int ApprovedDocuments { get; set; }
        public DateTime Create_at { get; set; }
        public string Tearcher { get; set; }=string.Empty;
    }
}
