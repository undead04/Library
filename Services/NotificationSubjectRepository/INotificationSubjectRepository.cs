using Library.DTO;
using Library.Model;
namespace Library.Services.NotificationSubjectRepository
{
    public interface INotificationSubjectRepository
    {
        Task CreatenotificationSubject(NoticationSubjectModel model);
        Task<List<NotificationSubjectDTO>> GetAllNotification(int subjectId, string? search, int? classRoomId);
    }
}
