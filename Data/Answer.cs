namespace Library.Data
{
    public class Answer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string context { get; set; } = string.Empty;
        public bool Istrue { get; set; } = true;
        public virtual Question? Question { get; set; }
        

    }

}
