using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Library.Model;
using Microsoft.AspNetCore.Authorization;
using Library.Services.JWTService;
using Library.Model.DTO;
using Library.Repository.NotificationSubjectRepository;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationSubjectController : ControllerBase
    {
        private readonly INotificationSubjectRepository repository;
        private readonly IJWTSevice jWTSevice;

        public NotificationSubjectController(INotificationSubjectRepository repository,IJWTSevice jWTSevice) 
        {
            this.repository = repository;
            this.jWTSevice = jWTSevice;
        }
        [HttpPost]
        [Authorize(Policy ="SubjectView")]
        public async Task<IActionResult> CreateNotification(NoticationSubjectModel model)
        {
            try
            {
                var userId = await jWTSevice.ReadToken();
                await repository.CreatenotificationSubject(model,userId);
                return Ok(BaseReponsitory<string>.WithMessage("Tạo thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("SubjectId/{Id}")]
        [Authorize(Policy = "SubjectView")]
        public async Task<IActionResult> GetAllNotification(int Id,string?search,int? classRoomId)
        {
            try
            {
                var notification= await repository.GetAllNotification(Id,search,classRoomId);
                return Ok(BaseReponsitory<List<NotificationSubjectDTO>>.WithData(notification, 200));
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
