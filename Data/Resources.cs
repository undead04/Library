using MimeKit.Tnef;

namespace Library.Data
{
    public class Resources
    {
        public int Id { get;set; }
        public int LessonId { get; set; }
        public int DoucmentId { get; set; }
        public virtual Lesson? Lesson { get; set; }
        public virtual Document? Document { get; set; }
        public ICollection<ClassResource> ClassResources { get; set; }

    }
}
