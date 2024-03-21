namespace Library.Data
{
    public class PrivateFile
    {
        public int Id { get; set; }
        public string CreateUserId { get; set; } = string.Empty;
        public string Type { get; set; }=string.Empty;
        public string Size { get; set;} = string.Empty;
        public string Name { get; set; }= string.Empty;
        public string Create_at { get; set;} = string.Empty;
        public virtual ApplicationUser? User { get; set; }
    }
}
