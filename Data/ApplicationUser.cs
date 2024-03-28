using Microsoft.AspNetCore.Identity;

namespace Library.Data
{
    public class ApplicationUser:IdentityUser
    {
        public string Avatar { get; set; } = string.Empty;
        public string Sex { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string UserCode { get; set; } = string.Empty;
        public virtual Student? Student { get; set; }
        public virtual Tearcher? Tearcher { get; set; }
        public ICollection<Subject>? Subjects { get; set; }
        public ICollection<QuestionSubject>? questionSubjects { get; set; }
        public ICollection<ReplyQuestion>? replyQuestions { get; set; }
        public ICollection<Exam>? exams { get; set; }
        public ICollection<NotificationSubject>? notificationSubjects { get; set; }
        public ICollection<Help>? helps { get; set; }
        public ICollection<Question>? questions { get; set; }
        public ICollection<Document>? documents { get; set; }
        public ICollection<PrivateFile>? privateFiles { get; set; }
        public ICollection<HistoryLike>? HistoryLikes { get; set; }
        public virtual SystemNotification? systemNotifications { get; set; }
        public ICollection<MyNotification>? myNotifications { get; set; }

       
    }
}
