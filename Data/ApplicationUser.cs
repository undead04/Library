using Microsoft.AspNetCore.Identity;

namespace Library.Data
{
    public class ApplicationUser:IdentityUser
    {
        public byte[]? Avatar { get; set; }
        public string Sex { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string UserCode { get; set; } = string.Empty;
       
        public int RoleId { get; set; }
      
        public virtual Role? Role { get; set; }
        public ICollection<Approve>? Approves { get;set; }
        public virtual Student? Student { get; set; }
        public virtual Tearcher? Tearcher { get; set; }
       
    }
}
