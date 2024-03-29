using Library.Model;
using Library.Repository.SystemRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Library.Model.DTO;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemController : ControllerBase
    {
        private readonly ISystemRepository systemRepository;

        public SystemController(ISystemRepository systemRepository) 
        {
            this.systemRepository = systemRepository;
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateSystem([FromForm]SystemModel model)
        {
            try
            {
                await systemRepository.CreateSystem(model);
                return Ok(BaseReponsitory<string>.WithMessage("Thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetSystem()
        {
            try
            {
                var system=  await systemRepository.GetSystem();
                return Ok(BaseReponsitory<SystemDTO>.WithData(system, 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateSystem([FromForm]SystemModel model)
        {
            try
            {
                await systemRepository.UpdateSystem(model);
                return Ok(BaseReponsitory<string>.WithMessage("Thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
