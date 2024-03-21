using Library.DTO;
using Library.Model;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Library.Services.ExamRepository
{
    public enum status
    {
        
        [Display(Name = "Đã hủy")]
        Cancel = 1,
        [Display(Name = "Đã phê duyệt")]
        Complete = 2,
        [Display(Name = "Chờ phê duyệt")]
        Wait = 3,
    }
    public interface IExamRepository
    {
        Task<List<ExamDTO>> GetAllExam(int? subjectId);
        Task CreateExamMultipleChoice(ExamMupliteChoiceModel model);
        Task<ExamDetailDTO> getExam(int Id);
        Task CreateExamEssay(ExamEssayModel model);
        Task<ExamEssayDTO> getExamEssay(int Id);
        Task RandomExam(RanDomExamModel model);

    }
}
