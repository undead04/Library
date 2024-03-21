using Library.DTO;
using Library.Model;
using Library.Services.ApproveDocumetService;
using Library.Services.DocumentRepository;
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

        public ApproveDocumentController(IApproveDocumetService approveDocumetService,IDocumentReponsitory documentReponsitory) 
        { 
            this.approveDocumetService=approveDocumetService;
            this.documentReponsitory = documentReponsitory;


        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Approve(int id,string userid)
        {
            try
            {
                var document = await documentReponsitory.GetByIdDocument(id);
                if (document == null)
                {
                    return NotFound();
                }
                await approveDocumetService.Approve(id,userid);
                return Ok(BaseReponsitory<string>.WithMessage("Phê duyệt thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("cancel/{id}")]
        public async Task<IActionResult> ApproveCancel(int id,ApproveCancelDocumentModel model)
        {
            try
            {
                var document = await documentReponsitory.GetByIdDocument(id);
                if (document == null)
                {
                    return NotFound();
                }
                await approveDocumetService.Cancel(id,model);
                return Ok(BaseReponsitory<string>.WithMessage("Phê duyệt thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
