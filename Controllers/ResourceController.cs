using Library.Model;
using Library.Services.ResourceReponsitory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Library.DTO;
using Microsoft.AspNetCore.Authorization;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        private readonly IResourceReponsitory resourceReponsitory;

        public ResourceController(IResourceReponsitory resourceReponsitory)
        {
            this.resourceReponsitory=resourceReponsitory;
        }
        [HttpPost]
        [Authorize(Policy ="DocumentCreate")]
        public async Task<IActionResult> CreateResource([FromForm]ResourceModel model)
        {
            try
            {
                await resourceReponsitory.CreateResource(model);
                return Ok(BaseReponsitory<string>.WithMessage("Thêm tài nguyên thành công",200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("lesson/{Id}")]
        [Authorize(Policy = "SubjectView")]
        public async Task<IActionResult> getAllResourceLesson(int Id)
        {
            try
            {
                var resource=  await resourceReponsitory.GetAllResourceLesson(Id);
                return Ok(BaseReponsitory<List<DTOResource>>.WithData(resource, 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{Id}")]
        [Authorize(Policy = "DocumentView")]
        public async Task<IActionResult> getResource(int Id)
        {
            try
            {
                var resource = await resourceReponsitory.GetResource(Id);
                if(resource!=null)
                {
                    return NotFound();
                }
                return Ok(BaseReponsitory<DTOResource>.WithData(resource, 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost("AddResourceDocument")]
        [Authorize(Policy = "DocumentView")]
        public async Task<IActionResult> AddResourceDocument(AddDocumnetResourceModel model)
        {
            try
            {
                 await resourceReponsitory.AddResourceDocument(model);
                
                return Ok(BaseReponsitory<string>.WithMessage("Thêm tài nguyên thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
