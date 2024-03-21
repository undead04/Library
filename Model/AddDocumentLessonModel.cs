namespace Library.Model
{
    public class AddDocumentLessonModel
    {
        public int DocumentId { get; set; }
        public int TopicId { get; set; }
        public int[] classId { get; set; }=new int[0];
        public string Title { get; set; } = string.Empty;
        
    }
}
