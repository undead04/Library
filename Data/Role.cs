using MimeKit.Encodings;
using System.ComponentModel.DataAnnotations;

namespace Library.Data
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Create_at { get;set; }
        public ICollection<ApplicationUser>? User { get; set; }

    }
}
