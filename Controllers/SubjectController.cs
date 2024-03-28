using Library.Data;
using Library.DTO;
using Library.Model;
using Library.Services.SubjectReponsitory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectReponsitory reponsitory;

        public SubjectController(ISubjectReponsitory reponsitory) 
        {
            this.reponsitory = reponsitory;
        }
        [HttpPost]
        [Authorize(Policy ="SubjectEdit")]
        public async Task<IActionResult> CreateSubject(SubjectModel model)
        {
            try
            {
                await reponsitory.CreateSubject(model);
                return Ok(BaseReponsitory<string>.WithMessage("Tạo môn học thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteSubject(int Id)
        {
            try
            {
                var subject = await reponsitory.GetById(Id);
                if(subject==null)
                {
                    return NotFound();
                }
                await reponsitory.DeleteSubject(Id);
                return Ok(BaseReponsitory<string>.WithMessage("Tạo môn học thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateSubject(int Id,SubjectModel model)
        {
            try
            {
                var subject = await reponsitory.GetById(Id);
                if (subject == null)
                {
                    return NotFound();
                }
                await reponsitory.UpdateSubject(Id,model);
                return Ok(BaseReponsitory<string>.WithMessage("Cập nhật môn học thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{Id}")]
        [Authorize(Policy = "SubjectView")]
        public async Task<IActionResult> getByIdSubject(int Id)
        {
            try
            {
                var subject = await reponsitory.GetById(Id);
                if (subject == null)
                {
                    return NotFound();
                }
                
                return Ok(BaseReponsitory<SubjectDTO>.WithData(subject, 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Authorize(Policy = "SubjectView")]
        public async Task<IActionResult> GetAllSubject(string? search, int? subjectId, string? orderBy, string? UserId, StatusDocument? statusDocument)
        {
            try
            {
                var subject = await reponsitory.GetAll(search,subjectId,orderBy,UserId,statusDocument);
                return Ok(BaseReponsitory<List<SubjectDTO>>.WithData(subject, 200));
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
