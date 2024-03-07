using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Library.Data
{
   
    public class Approve
    {
        public int Id { get; set; }
        public string ReponseUserId { get; set; } = string.Empty;
        public int DocumentId { get; set; }
       
        public DateTime ReponseDate { get; set; }
       
        public string Note { get; set; } = string.Empty;
        public virtual Document? document { get; set; }
        public virtual ApplicationUser? user { get; set; }
    }
}
