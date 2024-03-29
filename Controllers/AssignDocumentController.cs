using Library.Model;
using Library.Model.DTO;
using Library.Repository.ClassLessonRepository;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Policy ="DocumentAdd")]
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
