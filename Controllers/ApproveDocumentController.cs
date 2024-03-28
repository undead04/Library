using Library.DTO;
using Library.Model;
using Library.Services.ApproveDocumetService;
using Library.Services.DocumentRepository;
using Library.Services.JWTService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApproveDocumentController : ControllerBase
    {
        private readonly IApproveDocumetService approveDocumetService;
        private readonly IDocumentReponsitory documentReponsitory;
        private readonly IJWTSevice jwtService;

        public ApproveDocumentController(IApproveDocumetService approveDocumetService,IDocumentReponsitory documentReponsitory,IJWTSevice jWTSevice) 
        { 
            this.approveDocumetService=approveDocumetService;
            this.documentReponsitory = documentReponsitory;
            this.jwtService = jWTSevice;


        }
        [HttpPut("{id}")]
        [Authorize(Policy ="DocumentEdit")]
        public async Task<IActionResult> Approve(int id)
        {
            try
            {
                var document = await documentReponsitory.GetByIdDocument(id);
                if (document == null)
                {
                    return NotFound();
                }
                string userId = await jwtService.ReadToken();
                await approveDocumetService.Approve(id, userId);
                return Ok(BaseReponsitory<string>.WithMessage("Phê duyệt thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("cancel/{id}")]
        [Authorize(Policy = "DocumentEdit")]
        public async Task<IActionResult> ApproveCancel(int id, [FromBody] string note)
        {
            try
            {
                var document = await documentReponsitory.GetByIdDocument(id);
                if (document == null)
                {
                    return NotFound();
                }
                string userId = await jwtService.ReadToken();
                await approveDocumetService.Cancel(id,userId,note);
                return Ok(BaseReponsitory<string>.WithMessage("Phê duyệt thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
