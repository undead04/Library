using System.ComponentModel.DataAnnotations;

namespace Library.Data
{
    public enum StatusExam
    {
        [Display(Name = "Đã hủy")]
        Cancel = 1,
        [Display(Name = "Đã phê duyệt")]
        Complete = 2,
        [Display(Name = "Chờ phê duyệt")]
        Wait = 3,
        [Display(Name="Lưu nháp")]
        SaveDraft=4,



    }
    public class Exam
    {
        public int Id { get; set; }
        public string Form { get; set; } = string.Empty;
        public string Name { get; set; }=string.Empty;
        public string Time { get; set; }=string.Empty;
        public string UserId { get; set; }=string.Empty;
        public DateTime Create_At { get; set; }
        public string Status { get; set; } = StatusExam.Wait.ToString();
        public  int Subjectid { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public virtual Subject?Subject { get; set; }
        public ICollection<EssayExam>? essayExams { get; set; }
        public virtual ApplicationUser? applicationUsers { get; set; }
        public ICollection<QuestionExam>?QuestionExams { get; set; }

    }
}
