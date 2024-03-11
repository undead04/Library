using Library.DTO;
using Library.Server.DocumentRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

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
                var document=await reponsitory.GetAllDocumentSubject(SubjectId);
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
                
                var document = await reponsitory.GetByIdDocument(documentId);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Upload", document.Name);
                var provider = new FileExtensionContentTypeProvider();
                if(!provider.TryGetContentType(filePath, out var contexttype))
                {
                    contexttype = "application/octet-stream";
                }
                var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
                return File(bytes, contexttype,document.Name);
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
