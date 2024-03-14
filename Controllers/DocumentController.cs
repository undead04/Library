using Library.DTO;
using Library.Model;
using Library.Services.DocumentRepository;
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
        [HttpGet("Subject/{Id}")]
        public async Task<IActionResult> GetAllDocument(int Id)
        {
            try
            {
                var document=await reponsitory.GetAllDocumentSubject(Id);
                return Ok(BaseReponsitory<List<DocumentDTO>>.WithData(document, 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("download/{documentId}")]
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
        [HttpPost]
        public async Task<IActionResult> CreateLesson([FromForm]DocumentModel model)
        {
            try
            {
                await reponsitory.CreateDocumentLesson(model);
                return Ok(BaseReponsitory<string>.WithMessage("Thêm tài liệu thành công",200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> getDocuemnt(int Id)
        {
            try
            {
                var document= await reponsitory.GetByIdDocument(Id);
                if(document==null)
                {
                    return NotFound();
                }
                return Ok(BaseReponsitory<DocumentDTO>.WithData(document, 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllDocument(string typeDocument, string UserId)
        {
            try
            {
                var document = await reponsitory.GellAllDocument(typeDocument, UserId);
                return Ok(BaseReponsitory<List<DocumentDTO>>.WithData(document, 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut]
        public async Task<IActionResult> RenameDocument(int Id,string newName)
        {
            try
            {
                await reponsitory.RenameDocument(Id, newName);
                return Ok(BaseReponsitory<string>.WithMessage("Dổi tên thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
