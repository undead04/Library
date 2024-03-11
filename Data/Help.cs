namespace Library.Data
{
    public class Help
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string Context { get; set; }=string.Empty;
        public DateTime Create_At { get; set; }
        public ApplicationUser? applicationUsers { get; set; }

    }
}
