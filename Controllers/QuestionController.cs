using Library.DTO;
using Library.Model;
using Library.Services.MultipleChoiceRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IMultipleChoiceRepository repository;

        public QuestionController(IMultipleChoiceRepository repository)
        {
            this.repository = repository;
        }
        [HttpPost]
        public async Task<IActionResult> CreateQuestion(QuestionModel model)
        {
            try
            {
                await repository.CreateQuestion(model);
                return Ok(BaseReponsitory<string>.WithMessage("Tạo thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("subject/{subjectid}")]
        public async Task<IActionResult> CreateQuestion(int subjectid,string?level)
        {
            try
            {
                var question= await repository.GetAllQuestion(subjectid,level);
                return Ok(BaseReponsitory<List<QuestionDTO>>.WithData(question, 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteQuestion(int Id)
        {
            try
            {
                var question = await repository.GetQuestion(Id);
                if(question==null)
                {
                    return NotFound();
                }
                await repository.DeleteQuestion(Id);

                return Ok(BaseReponsitory<string>.WithMessage("Xóa thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetQuestion(int Id)
        {
            try
            {
                var question = await repository.GetQuestion(Id);
                if (question == null)
                {
                    return NotFound();
                }
                

                return Ok(BaseReponsitory<QuestionDetail>.WithData(question, 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateQuestin(int Id,QuestionModel model)
        {
            try
            {
                var question = await repository.GetQuestion(Id);
                if (question == null)
                {
                    return NotFound();
                }
                await repository.UpdateQuestion(Id, model);

                return Ok(BaseReponsitory<string>.WithMessage("Cập nhập thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
