using Library.Model;
using Library.Model.DTO;

namespace Library.Repository.SystemNotificationRepository
{
    public interface ISystemNotificationRepository
    {
        Task CreateSystemNotification(SystemNotificationModel model);
        Task UpdateSystemNotification(int id, SystemNotificationModel model);
        Task<SystemNotificationUserDTO> GetSystemNotification(string userId);
        Task DeleteSystemNotification(string userId);
    }
}
