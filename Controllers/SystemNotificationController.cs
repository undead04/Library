using Library.Model;
using Library.Model.DTO;
using Library.Repository.SystemNotificationRepository;
using Library.Services.JWTService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemNotificationController : ControllerBase
    {
        private readonly ISystemNotificationRepository systemNotificationRepository;
        private readonly IJWTSevice jWTSevice;

        public SystemNotificationController(ISystemNotificationRepository systemNotificationRepository,IJWTSevice jWTSevice) 
        {
            this.systemNotificationRepository = systemNotificationRepository;
            this.jWTSevice = jWTSevice;
        }
        [HttpGet]
        [Authorize(Policy = "NotificationSystem")]
        public async Task<IActionResult> getSystemNotification()
        {
            try
            {
                var userId = await jWTSevice.ReadToken();
                var systemNotification = await systemNotificationRepository.GetSystemNotification(userId);
                return Ok(BaseReponsitory<SystemNotificationUserDTO>.WithData(systemNotification,200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("{Id}")]
        [Authorize(Policy = "NotificationSystem")]
        public async Task<IActionResult> UpdateSystemNotificaton(int Id,SystemNotificationModel model)
        {
            try
            {
                var userId = await jWTSevice.ReadToken();
                var systemNotification = await systemNotificationRepository.GetSystemNotification(userId);
                if(systemNotification==null)
                {
                    return NotFound();
                }
                await systemNotificationRepository.UpdateSystemNotification(Id, model);
                return Ok(BaseReponsitory<string>.WithMessage("Cập nhật thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
