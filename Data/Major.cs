namespace Library.Data
{
    public class Major
    {
        public int Id { get;set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; }=string.Empty;
        public ICollection<Subject> Subjects { get; set; }
        public ICollection<Student>? students { get; set; }
        public ICollection<Tearcher>? tearchers { get; set; }


    }
}
