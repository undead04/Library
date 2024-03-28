using Org.BouncyCastle.Bcpg.OpenPgp;

namespace Library.DTO
{
    public class UserDTO
    {
        public string Id { get; set; } = string.Empty;
        public string CodeUser { get; set; } = string.Empty;
        public string Sex { get;set; }=string.Empty;
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; }=string.Empty;
        public string Phone { get;set; } = string.Empty;
        public string Address { get; set; }=string.Empty;
        public string Avatar { get; set; } = string.Empty;
        public string UrlAvatar { get; set; }=string.Empty;
        public string Role { get; set; }=string.Empty;
        public string Class { get; set; } = string.Empty;
        public string Major { get; set; } = string.Empty;
        
    }
}
