using Library.DTO;
using Library.Model;
using Library.Services.HelpRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelpController : ControllerBase
    {
        private readonly IHelpRepository repository;

        public HelpController(IHelpRepository repository) 
        {
            this.repository = repository;
        }
        [HttpPost]
        public async Task<IActionResult> CreateHelp(HelpModel model)
        {
            try
            {
                await repository.CreateHelp(model);
                return Ok(BaseReponsitory<string>.WithMessage("Phản hồi thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllHelp()
        {
            try
            {
                var help= await repository.GetAllHelp();
                return Ok(BaseReponsitory<List<HelpDTO>>.WithData(help, 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetHelp(int Id)
        {
            try
            {
                var help = await repository.GetHelpById(Id);
                if(help==null)
                {
                    return NotFound();
                }
                return Ok(BaseReponsitory<HelpDTO>.WithData(help, 200));
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
