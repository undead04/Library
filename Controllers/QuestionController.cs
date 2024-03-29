using Library.Model;
using Library.Model.DTO;
using Library.Repository.MultipleChoiceRepository;
using Library.Services.ExcelService;
using Library.Services.JWTService;
using Library.Services.UploadService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IMultipleChoiceRepository repository;
        private readonly IExcelService excelService;
        private readonly IUploadService uploadService;
        private readonly IJWTSevice jWTSevice;

        public QuestionController(IMultipleChoiceRepository repository, IExcelService excelService,IUploadService uploadService,IJWTSevice jWTSevice)
        {
            this.repository = repository;
            this.excelService = excelService;
            this.uploadService = uploadService;
            this.jWTSevice = jWTSevice;
        }
        [HttpPost]
        [Authorize(Policy ="ExamCreate")]
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
        [HttpPost("Upload")]
        [Authorize(Policy = "ExamCreate")]
        public async Task<IActionResult> UploadQuestion([FromForm]UploadQuestionModel model)
        {
            try
            {
                var CreateUserId = await jWTSevice.ReadToken();
                string nameFile = await uploadService.UploadImage("Question", model.File!);
                string filePath = uploadService.GetFilePath("Question", nameFile);
                await excelService.GetExcel(model.SubjectId, CreateUserId, filePath);
                return Ok(BaseReponsitory<string>.WithMessage("Tạo thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("subject/{subjectid}")]
        [Authorize(Policy = "ExamView")]
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
        [Authorize(Policy = "ExamDelete")]
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
        [Authorize(Policy = "ExamView")]
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
        [Authorize(Policy = "ExamDelete")]
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
