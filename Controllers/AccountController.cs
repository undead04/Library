using Library.Data;
using Library.DTO;
using Library.Model;
using Library.Services.AccountReponsitory;
using Microsoft.AspNetCore.Mvc;



namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountReponsitory accountRepo;
        private readonly MyDB context;

        public AccountController(IAccountReponsitory repo,MyDB context)
        {
            accountRepo = repo;
           
        }

       

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(SignInModel signInModel)
        {
            try
            {
                var result = await accountRepo.SignInAsync(signInModel);

                if (string.IsNullOrEmpty(result))
                {
                    return BadRequest(BaseReponsitory<string>.WithMessage("Mật khẩu hoặc tên đăng nhập sai", 400));
                }

                return Ok(BaseReponsitory<string>.WithData(result,200));
            }
            catch
            {
                return BadRequest();
            }
        }
       
    }
}

