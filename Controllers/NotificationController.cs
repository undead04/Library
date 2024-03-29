using Library.Model.DTO;
using Library.Repository.MyNotificationRepository;
using Library.Services.JWTService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private IMyNotificationRepository myNotificationRepository;
        private readonly IJWTSevice jWTSevice;


        public NotificationController(IMyNotificationRepository myNotificationRepository, IJWTSevice jWTSevice)
        {
            this.myNotificationRepository = myNotificationRepository;
            this.jWTSevice = jWTSevice;

        }
        [HttpGet]
        [Authorize(Policy = "NotificationView")]
        public async Task<IActionResult> ListNotification(bool? isRead)
        {
            try
            {
                var userId = await jWTSevice.ReadToken();
                var notifications = await myNotificationRepository.ListNotification(userId, isRead);
                return Ok(BaseReponsitory<List<NotificationDTO>>.WithData(notifications, 200));

            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("{notificationId}")]
        [Authorize(Policy = "NotificationDelete")]
        public async Task<IActionResult> DeleteNotification(int notificationId)
        {
            try
            {
                var userId = await jWTSevice.ReadToken();
                var notification = await myNotificationRepository.GetNotification(userId, notificationId);
                if (notification == null)
                {
                    return NotFound();
                }
                await myNotificationRepository.DeleteNotification(userId, notificationId);
                return Ok(BaseReponsitory<string>.WithMessage("Xóa thành công", 200));

            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("{notificationId}")]
        [Authorize(Policy = "NotificationEdit")]
        public async Task<IActionResult> ReadNotification(int notificationId)
        {
            try
            {
                var userId = await jWTSevice.ReadToken();
                var notification = await myNotificationRepository.GetNotification(userId,notificationId);
                if (notification == null)
                {
                    return NotFound();
                }
                await myNotificationRepository.ReadNotification(userId, notificationId);
                return Ok(BaseReponsitory<string>.WithMessage("Đọc thành công", 200));

            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
