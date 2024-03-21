using Library.DTO;
using Library.Services.MySubjectService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MySubjectClassController : ControllerBase
    {
        private readonly IMySubjectService subjectService;

        public MySubjectClassController(IMySubjectService subjectService) 
        {
            this.subjectService = subjectService;
        
        }
        [HttpGet("{classId}")]
        public async Task<IActionResult> GetAllMySubject(int classId)
        {
            try
            {
                var subjects =await subjectService.SeenMySubjectStudent(classId);
                return Ok(BaseReponsitory<List<MySubjectStudentDTO>>.WithData(subjects, 200));
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
