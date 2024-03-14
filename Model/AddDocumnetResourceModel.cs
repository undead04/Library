namespace Library.Model
{
    public class AddDocumnetResourceModel
    {
        public int LessonId { get; set; }
        public List<int> DocumnetId { get; set; }=new List<int>();
    }
}
