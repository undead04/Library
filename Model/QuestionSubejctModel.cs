namespace Library.Model
{
    public class QuestionSubejctModel
    {
        public string Title { get; set; } = string.Empty;
        public string context { get; set; }=string.Empty;
        public int [] ClassRoomId { get; set; }=new int[0];
        public int LessonId { get; set; }

    }
}
