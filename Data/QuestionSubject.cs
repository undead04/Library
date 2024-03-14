using Org.BouncyCastle.Bcpg;

namespace Library.Data
{
    public class QuestionSubject
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public DateTime Create_At { get; set; }
        public int LessonId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Context { get; set; } = string.Empty;
        public int Like { get; set; }=0;
        public ICollection<QuestionClassRoom>? questionClassRooms { get; set; }
        public virtual Lesson? Lesson { get; set; }
        public ICollection<ReplyQuestion>? replyQuestions { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }

    }
}
