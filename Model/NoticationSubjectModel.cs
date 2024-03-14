namespace Library.Model
{
    public class NoticationSubjectModel
    {
        public string CreateUserId { get; set; } = string.Empty;
        public string Title { get;set; }=string.Empty;
        public string Context { get; set; }=string.Empty;
        public int SubjectId { get; set; }
        public int[] ClassRoomId { get; set; }=new int[0];
    }
}
