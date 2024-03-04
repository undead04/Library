using Library.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly ClaimsPrincipal _user;
        private readonly UserManager<ApplicationUser> userManager;

        public ImageController(IHttpContextAccessor httpContextAccessor,UserManager<ApplicationUser> userManager) 
        {
            _user = httpContextAccessor.HttpContext!.User;
            this.userManager = userManager;
        }
        [HttpGet]
        //Để test api update Image có hoạt động hay không
        public async Task<IActionResult> Image()
        {
            var user = userManager.FindByEmailAsync(_user.FindFirst(ClaimTypes.Email).Value).Result;
            if(user.Avatar==null)
            {
                return BadRequest("Không có hình ảnh");
            }
            return File(user.Avatar, "image/jpeg");
        }
    }
}
