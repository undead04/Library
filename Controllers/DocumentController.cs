using Library.Data;
using Library.Model;
using Library.Model.DTO;
using Library.Repository.DocumentRepository;
using Library.Services.UploadService;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IUploadService upload;

        public DocumentController(IDocumentReponsitory reponsitory,IUploadService upload)
        {
            this.reponsitory = reponsitory;
            this.upload = upload;
        }


        [HttpDelete("{documentId}")]
        [Authorize(Policy = "DocumentDelete")]
        public async Task<IActionResult> DeleteDocument(int documentId)
        {
            try
            {
                await reponsitory.DeleteDoucment(documentId);
                return Ok(BaseReponsitory<string>.WithMessage("Xóa document Thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Authorize(Policy = "DocumentCreate")]
        public async Task<IActionResult> CreateLesson([FromForm] DocumentModel model)
        {
            try
            {
                await reponsitory.CreateDocumentLesson(model);
                return Ok(BaseReponsitory<string>.WithMessage("Thêm tài liệu thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{Id}")]
        [Authorize(Policy = "DocumentView")]
        public async Task<IActionResult> getDocuemnt(int Id)
        {
            try
            {
                var document = await reponsitory.GetByIdDocument(Id);
                if (document == null)
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
        [Authorize(Policy = "DocumentView")]
        public async Task<IActionResult> GetAllDocument(string? typeDocument, string? UserId, int? subjectid, StatusDocument? statusDocument)
        {
            try
            {
                var document = await reponsitory.GellAllDocument(typeDocument, UserId, subjectid, statusDocument);
                return Ok(BaseReponsitory<List<DocumentDTO>>.WithData(document, 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut]
        [Authorize(Policy = "DocumentEdit")]
        public async Task<IActionResult> RenameDocument(int Id, string newName)
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
        [HttpGet("download/{Id}")]
        [Authorize(Policy = "DocumentDownload")]
        public async Task<IActionResult> DownloadDocument(int Id)
        {
            try {
                var document = await reponsitory.GetByIdDocument(Id);
                if(document==null)
                {
                    return NotFound();
                }
                var result = await upload.DownloadFile(document.Name,"Document");
                return File(result.Item1, result.Item2, result.Item3);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
