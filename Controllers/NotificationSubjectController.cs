using Library.Services.NotificationSubjectRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Library.Model;
using Library.DTO;
namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationSubjectController : ControllerBase
    {
        private readonly INotificationSubjectRepository repository;

        public NotificationSubjectController(INotificationSubjectRepository repository) 
        {
            this.repository = repository;
        }
        [HttpPost]
        public async Task<IActionResult> CreateNotification(NoticationSubjectModel model)
        {
            try
            {
                await repository.CreatenotificationSubject(model);
                return Ok(BaseReponsitory<string>.WithMessage("Tạo thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("SubjectId/{Id}")]
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
