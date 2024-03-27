using Library.DTO;
using Library.Model;

namespace Library.Services.SystemNotificationRepository
{
    public interface ISystemNotificationRepository
    {
        Task CreateSystemNotification(SystemNotificationModel model);
        Task UpdateSystemNotification(int id,SystemNotificationModel model);
        Task<SystemNotificationUserDTO> GetSystemNotification(int id);
        Task DeleteSystemNotification(int Id);
    }
}
