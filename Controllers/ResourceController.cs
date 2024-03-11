using Library.Model;
using Library.Server.ResourceReponsitory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Library.DTO;
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
        public async Task<IActionResult> CreateResource(ResourceModel model)
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
