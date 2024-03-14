using Library.DTO;
using Library.Model;
using Library.Services.QuestionRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionSubejctController : ControllerBase
    {
        private readonly IQuestionSubjectRepository questionSubjectRepository;

        public QuestionSubejctController(IQuestionSubjectRepository questionSubjectRepository) 
        { 
            this.questionSubjectRepository=questionSubjectRepository;
        }
        [HttpPost]
        public async Task<IActionResult> CreateQuestionSubject(QuestionSubejctModel model)
        {
            try
            {
                await questionSubjectRepository.CreateQuestionSubject(model);
                return Ok(BaseReponsitory<string>.WithMessage("Tạo câu hỏi thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("subject/{Id}")]
        public async Task<IActionResult> GetAll(int Id,string? search, int? classRoomId, int? lessonId, string? OrderBy, string? FilterQuestion)
        {
            try
            {
                var question= await questionSubjectRepository.GetAllQuestionSubject(Id,search,classRoomId,lessonId,OrderBy,FilterQuestion);
                return Ok(BaseReponsitory<List<QuestionSubjectDTO>>.WithData(question, 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("Like/{Id}")]
        public async Task<IActionResult> LikeQuestion(int Id)
        {
            try
            {
                var question = await questionSubjectRepository.GetQuestionSubject(Id);
                if(question == null)
                {
                    return NotFound();
                }
                await questionSubjectRepository.LikeQuestion(Id);
                return Ok(BaseReponsitory<string>.WithMessage("Thích thành công", 200));
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
                var question = await questionSubjectRepository.GetQuestionSubject(Id);
                if (question == null)
                {
                    return NotFound();
                }
                
                return Ok(BaseReponsitory<QuestionSubjectDTO>.WithData(question, 200));
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
