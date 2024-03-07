using Library.DTO;
using Library.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolLibrary.DTO;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentReponsitory reponsitory;

        public DocumentController(IDocumentReponsitory reponsitory) 
        {
            this.reponsitory = reponsitory;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllDocument(int? SubjectId)
        {
            try
            {
                var document=await reponsitory.GetAllDocuments(SubjectId);
                return Ok(BaseReponsitory<List<DocumentDTO>>.WithData(document, 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{documentId}")]
        public async Task<IActionResult> GetFile(int documentId)
        {
            try
            {
                var filePath = await reponsitory.GetDoucment(documentId);
                var document = await reponsitory.GetByIdDocument(documentId);
                return File(filePath, "application/octet-stream",document.Name);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("{documentId}")]
        public async Task<IActionResult> DeleteDocument(int documentId)
        {
            try
            {
                await reponsitory.DeleteDoucment(documentId);
                return Ok(BaseReponsitory<string>.WithMessage("Xóa document Thành công",200));
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
