using System.ComponentModel.DataAnnotations;

namespace Library.Data
{
    public class HistoryLike
    {
        public int SubjectQuestionId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public virtual ApplicationUser? User { get; set; }
        public virtual QuestionSubject? QuestionSubject { get; set; }
    }
}
