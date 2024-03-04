using Microsoft.AspNetCore.Identity;

namespace Library.Data
{
    public class ApplicationUser:IdentityUser
    {
        public byte[]? Avatar { get; set; }
        public string Sex { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string MaUser { get; set; } = string.Empty;
        public string Majors { get; set; } = string.Empty;
    }
}
