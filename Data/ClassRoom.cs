namespace Library.Data
{
    public class ClassRoom
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CodeClassRoom { get;set; }=string.Empty;
        public ICollection<SubjectClassRoom>? subjectClassRooms { get; set; }
        public ICollection<Student>? students { get; set; } 
        

    }
}
