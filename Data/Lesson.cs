namespace Library.Data
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int TopicId { get;set; }
        public int DoucumentId { get; set; }
        public ICollection<ClassLesson>? ClassLessons { get; set; }
        public virtual Topic? topic { get; set; }
        public ICollection<Resources>? Resources { get; set; }
        public virtual Document? Document { get; set; }
        public ICollection<QuestionSubject>? questionSubjects { get; set; }
    }
}
