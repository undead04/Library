namespace Library.Model
{
    public class AddDocumnetResourceModel
    {
        public int LessonId { get; set; }
        public int[] ClassId { get; set; }=new int[0];
        public List<int> DocumnetId { get; set; }=new List<int>();
    }
}
