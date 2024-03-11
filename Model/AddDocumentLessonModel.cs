namespace Library.Model
{
    public class AddDocumentLessonModel
    {
        public int DocumentId { get; set; }
        public int TopicId { get; set; }
       
        public string Title { get; set; } = string.Empty;
    }
}
