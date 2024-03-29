using Library.Model;
using Library.Model.DTO;

namespace Library.Repository.NotificationSubjectRepository
{
    public interface INotificationSubjectRepository
    {
        Task CreatenotificationSubject(NoticationSubjectModel model, string userId);
        Task<List<NotificationSubjectDTO>> GetAllNotification(int subjectId, string? search, int? classRoomId);
    }
}
