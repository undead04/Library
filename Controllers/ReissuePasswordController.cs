using Library.Model;
using Library.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using SchoolLibrary.DTO;

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
