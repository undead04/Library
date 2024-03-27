using Library.Data;
using Library.DTO;
using Library.Model;
using Library.Services.ExamRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly IExamRepository repository;

        public ExamController(IExamRepository repository) 
        { 
            this.repository=repository;
        }
        [HttpPost("Multiple")]
        public async Task<IActionResult> CreateExam(ExamMupliteChoiceModel model)
        {
            try
            {
                await repository.CreateExamMultipleChoice(model);
                return Ok(BaseReponsitory<string>.WithMessage("Tạo thành công",200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost("Essay")]
        public async Task<IActionResult> CreateExamEssay(ExamEssayModel model)
        {
            try
            {
                await repository.CreateExamEssay(model);
                return Ok(BaseReponsitory<string>.WithMessage("Tạo thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllExam (int? subjectId,string? teacherId,status?status)
        {
            try
            {
                var exam= await repository.GetAllExam(subjectId,teacherId,status);
                return Ok(BaseReponsitory<List<ExamDTO>>.WithData(exam, 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("muplite/{Id}")]
        public async Task<IActionResult> GetExamMuplite(int Id)
        {
            try
            {
                var exam = await repository.getExam(Id);
                if (exam == null) return NotFound();
                return Ok(BaseReponsitory<ExamDetailDTO>.WithData(exam, 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("Essay/{Id}")]
        public async Task<IActionResult> GetExamEssay(int Id)
        {
            try
            {
                var exam = await repository.getExamEssay(Id);
                if (exam == null) return NotFound();
                return Ok(BaseReponsitory<ExamEssayDTO>.WithData(exam, 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost("RandomExam")]
        public async Task<IActionResult> RandomExam(RanDomExamModel model)
        {
            try
            {
                 await repository.RandomExam(model);
                
                return Ok(BaseReponsitory<string>.WithMessage("Tạo thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost("UploadExam")]
        public async Task<IActionResult> UploadExam([FromForm]ExamModel model)
        {
            try
            {
                await repository.UploadExam(model);

                return Ok(BaseReponsitory<string>.WithMessage("Tạo thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExam(int id)
        {
            try
            {
                var exam =await repository.getExam(id);
                if (exam == null) return NotFound();
                await repository.DeleteExam(id);

                return Ok(BaseReponsitory<string>.WithMessage("Xóa thành công thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> DeleteExam(int id,string name)
        {
            try
            {
                var exam = await repository.getExam(id);
                if (exam == null) return NotFound();
                await repository.RenameFile(id,name);

                return Ok(BaseReponsitory<string>.WithMessage("Đổi tên thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
