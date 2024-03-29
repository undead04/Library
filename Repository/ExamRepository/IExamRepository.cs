using Library.Model;
using Library.Model.DTO;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Library.Repository.ExamRepository
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
        Task<List<ExamDTO>> GetAllExam(int? subjectId, string? teacherId, status? status);
        Task CreateExamMultipleChoice(ExamMupliteChoiceModel model);
        Task<ExamDetailDTO> getExam(int Id);
        Task CreateExamEssay(ExamEssayModel model);
        Task<ExamEssayDTO> getExamEssay(int Id);
        Task RandomExam(RanDomExamModel model);
        Task UploadExam(ExamModel model);
        Task DeleteExam(int id);
        Task RenameFile(int id, string name);

    }
}
