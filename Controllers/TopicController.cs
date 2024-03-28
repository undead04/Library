using Library.DTO;
using Library.Model;
using Library.Services.SubjectReponsitory;
using Library.Services.TopicReponsitory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private readonly ITopicReponsitory reponsitory;
        private readonly ISubjectReponsitory subjectReponsitory;

        public TopicController(ITopicReponsitory reponsitory,ISubjectReponsitory subjectReponsitory) 
        {
            this.reponsitory = reponsitory;
            this.subjectReponsitory=subjectReponsitory;
        }
        [HttpPost]
        [Authorize(Policy ="SubjectEdit")]
        public async Task<IActionResult> CreateTopic(TopicModel model)
        {
            try
            {
                await reponsitory.CreateTopic(model);
                return Ok(BaseReponsitory<string>.WithMessage("Tạo chủ đề thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("{Id}")]
        [Authorize(Policy = "SubjectEdit")]
        public async Task<IActionResult> DeleteTopic(int Id)
        {
            try
            {
                var topic = await reponsitory.GetTopicById(Id);
                if(topic==null)
                {
                    return NotFound();
                }
                await reponsitory.DeleteTopic(Id);
                return Ok(BaseReponsitory<string>.WithMessage("Xóa chủ đề thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("{Id}")]
        [Authorize(Policy = "SubjectEdit")]
        public async Task<IActionResult> UpdateTopic(int Id,TopicModel model)
        {
            try
            {
                var topic = await reponsitory.GetTopicById(Id);
                if (topic == null)
                {
                    return NotFound();
                }
                await reponsitory.UpdateTopic(Id,model);
                return Ok(BaseReponsitory<string>.WithMessage("Cập nhật chủ đề thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("Subject/{Id}")]
        [Authorize(Policy = "SubjectView")]
        public async Task<IActionResult> GetAllTopic(int Id)
        {
            try
            {
                var subject = await subjectReponsitory.GetById(Id);
                if (subject == null)
                {
                    return NotFound();
                }
               var topic= await reponsitory.GetAll(Id);
                return Ok(BaseReponsitory<List<TopicDTO>>.WithData(topic, 200));
            }
            catch
            {
                return BadRequest();
            }
        }



    }
}
