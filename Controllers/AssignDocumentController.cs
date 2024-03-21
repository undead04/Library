using Library.DTO;
using Library.Model;
using Library.Services.ClassLessonRepository;

using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignDocumentController : ControllerBase
    {
        private readonly IClassLessonRepository repository;

        public AssignDocumentController(IClassLessonRepository repository) 
        {
            this.repository=repository;
        }
        [HttpPost]
        public async Task<IActionResult> CreateAssignDocument(AssignDocumentModel model)
        {
            try
            {
                await repository.AssignDocuments(model);
                return Ok(BaseReponsitory<string>.WithMessage("Phân công tài liệu thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
