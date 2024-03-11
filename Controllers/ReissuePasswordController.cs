using Library.DTO;
using Library.Model;
using Library.Server.ReissuePassword;
using Microsoft.AspNetCore.Mvc;



namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReissuePasswordController : ControllerBase
    {
        private readonly IReissuePassword reissuePassword;

        public ReissuePasswordController(IReissuePassword reissuePassword)
        { 
            this.reissuePassword=reissuePassword;
        }
        [HttpPost]
        public async Task<IActionResult> SendEmail(ReissuePasswordModel model)
        {
            try
            {
               await reissuePassword.ReissuePassword(model);
                return Ok(BaseReponsitory<string>.WithMessage("Cấp mật khẩu thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
