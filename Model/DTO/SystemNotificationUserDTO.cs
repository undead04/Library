namespace Library.Model.DTO
{
    public class SystemNotificationUserDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public bool IsUpdateInformationUser { get; set; } = false;
        public bool IsChangePassword { get; set; } = false;
        public bool IsChangleListRole { get; set; } = false;
        public bool IsChangeLeListUser { get; set; } = false;
        public bool IsCancelExam { get; set; } = false;
        public bool IsSaveExam { get; set; } = false;
        public bool IsCrudPrivateFile { get; set; } = false;
        public bool IsContentSubject { get; set; } = false;
        public bool IsListSUbject { get; set; } = false;
        public bool IsCreateNotificationSubject { get; set; } = false;
        public bool IsCommentNotification { get; set; } = false;
        public bool IsCommentMyQuestion { get; set; } = false;
        public bool IsTeacherQuestionSubject { get; set; } = false;
        public bool IscrudExam { get; set; } = false;
        public bool IscrudQuestion { get; set; } = false;
        public bool IscrudLesson { get; set; } = false;
        public bool IscrudResource { get; set; } = false;
        public bool IscrudDocument { get; set; } = false;

    }

}
