namespace Library.Data
{
    public class ClassLesson
    {
        public int Id { get; set; }
        public int ClassId { get; set; }
        public int LessonId { get; set; }
        public virtual ClassRoom? ClassRoom { get; set; }
        public virtual Lesson? Lesson { get; set; }
    }
}
