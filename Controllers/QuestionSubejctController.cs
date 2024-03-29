using Library.Model;
using Library.Model.DTO;
using Library.Repository.HistoryLikeRepository;
using Library.Repository.QuestionSubjectRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionSubejctController : ControllerBase
    {
        private readonly IQuestionSubjectRepository questionSubjectRepository;
        private readonly IHistoryLikeRepository historyLikeRepository;

        public QuestionSubejctController(IQuestionSubjectRepository questionSubjectRepository,IHistoryLikeRepository historyLikeRepository) 
        { 
            this.questionSubjectRepository=questionSubjectRepository;
            this.historyLikeRepository=historyLikeRepository;
        }
        [HttpPost]
        [Authorize(Policy ="SubjectView")]
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
        [Authorize(Policy = "SubjectView")]
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
        [Authorize(Policy = "SubjectEdit")]
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
        [Authorize(Policy = "SubjectView")]
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
