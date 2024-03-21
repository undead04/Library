namespace Library.Data
{
    public class ClassResource
    {
        public int Id { get; set; }
        public int ClassId { get; set; }
        public int ResourceId { get; set; }
        public virtual ClassRoom? ClassRoom { get; set; }
        public virtual Resources? Resources { get; set; }
    }
}
