using Library.Model.DTO;
using Library.Repository.ExamRepository;
using Library.Services.ApproveExamServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApproveExamController : ControllerBase
    {
        private readonly IApporveExamServices apporveExamServices;
        private readonly IExamRepository examRepository;

        public ApproveExamController(IApporveExamServices apporveExamServices,IExamRepository examRepository) 
        {
            this.apporveExamServices = apporveExamServices;
            this.examRepository = examRepository;
        }
        [HttpPut("{id}")]
        [Authorize(Policy = "ExamApprove")]
        public async Task<IActionResult> Approve(int id)
        {
            try
            {
                var exam = await examRepository.getExam(id);
                if(exam==null)
                {
                    return NotFound();
                }
                if(exam.Status!=status.Wait.ToString())
                {
                    return BadRequest(BaseReponsitory<string>.WithMessage("Đề thi đã đc phê duyệt rồi", 400));
                }
                await apporveExamServices.Approve(id);
                return Ok(BaseReponsitory<string>.WithMessage("Thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("Cancel/{id}")]
        [Authorize(Policy = "ExamApprove")]
        public async Task<IActionResult> Cancel(int id)
        {
            try
            {
                var exam = await examRepository.getExam(id);
                if (exam == null)
                {
                    return NotFound();
                }
                if (exam.Status != status.Wait.ToString())
                {
                    return BadRequest(BaseReponsitory<string>.WithMessage("Đề thi đã đc phê duyệt rồi", 400));
                }
                await apporveExamServices.Cancel(id);
                return Ok(BaseReponsitory<string>.WithMessage("Thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
