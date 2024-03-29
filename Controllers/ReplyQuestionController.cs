using Library.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Authorization;
using Library.Model.DTO;
using Library.Repository.ReplyQuestionRepository;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReplyQuestionController : ControllerBase
    {
        private readonly IReplyQuestionRepository repository;

        public ReplyQuestionController(IReplyQuestionRepository repository) 
        {
            this.repository = repository;
        }
        [HttpPost]
        [Authorize(Policy ="SubjectEdit")]
        public async Task<IActionResult> CreateReplyQuestion(ReplyModel model)
        {
            try
            {
                await repository.CreateReplyQuestion(model);
                return Ok(BaseReponsitory<string>.WithMessage("Trả lời thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("Question/{Id}")]
        [Authorize(Policy = "SubjectView")]
        public async Task<IActionResult> GetAllReplyQuestion(int Id)
        {
            try
            {
                var reply= await repository.GetAllReplyQuestion(Id);
                return Ok(BaseReponsitory<List<ReplyDTO>>.WithData(reply, 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{Id}")]
        [Authorize(Policy = "SubjectView")]
        public async Task<IActionResult> getReplyQuestion(int Id)
        {
            try
            {
                var reply = await repository.GetReplyQuestion(Id);
                if (reply == null)
                {
                    return NotFound();
                }
                return Ok(BaseReponsitory<ReplyDTO>.WithData(reply, 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("{Id}")]
        [Authorize(Policy = "SubjectEdit")]
        public async Task<IActionResult> DeleteReplyQuestion(int Id)
        {
            try
            {
                var reply = await repository.GetReplyQuestion(Id);
                if (reply == null)
                {
                    return NotFound();
                }
                await repository.DeleteReplyQuestion(Id);
                return Ok(BaseReponsitory<string>.WithMessage("Xóa thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("{Id}")]
        [Authorize(Policy = "SubjectEdit")]
        public async Task<IActionResult> UpdateReplyQuestion(int Id,ReplyUpdateModel model)
        {
            try
            {
                var reply = await repository.GetReplyQuestion(Id);
                if (reply == null)
                {
                    return NotFound();
                }
                await repository.UpdateReplyQuestion(Id,model);
                return Ok(BaseReponsitory<string>.WithMessage("Cập nhật thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
        }


    }
}
