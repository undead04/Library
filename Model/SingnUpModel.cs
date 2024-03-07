namespace Library.Model
{
    public class SingnUpModel
    {
       
        public string Sex { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string UserCode { get; set; } = string.Empty;
        public int MajorsId { get; set; }
        public int ClassRoomId { get; set; }
        public int RoleId { get; set; }
        public string Email { get;set; } = string.Empty;
        public string UserName { get;set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}
