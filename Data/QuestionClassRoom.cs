namespace Library.Data
{
    public class QuestionClassRoom
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int ClassRoomId { get; set; }
        public virtual QuestionSubject? QuestionSubject { get; set; }
        public virtual ClassRoom? ClassRoom { get; set; }
    }
}
