using Library.Model;

namespace Library.DTO
{
    public class SubjectDTO: SubjectModel
    {
        public int Id { get; set; }
        public string MajorName { get; set; } = string.Empty;
       
    }
}
