using System.ComponentModel.DataAnnotations;

namespace Library.Model
{
    public class SubjectModel
    {
        
        public string Name { get; set; } = string.Empty;
        public int MajorId { get; set; }
        public string SubjectCode { get; set; } = string.Empty;
        public string Describe { get; set; } = string.Empty;
        public string UserId { get; set; }=string.Empty;
       
       
    }
}
