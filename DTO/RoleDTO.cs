namespace Library.DTO
{
    public class RoleDTO
    {
        public string Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Create_at { get; set; }
    }
}
