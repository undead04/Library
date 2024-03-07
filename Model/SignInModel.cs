namespace SchoolLibrary.Model
{
    public class SignInModel
    {
        public int RoleId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get;set; }=string.Empty;
    }
  
}
