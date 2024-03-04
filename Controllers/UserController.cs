using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolLibrary.DTO;
using SchoolLibrary.Model;
using SchoolLibrary.Server;
using SchoolLibrary.Validation;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserReponsitory userReponsitory;
        private readonly ChanglePassWordValidation validation;

        public UserController(IUserReponsitory userReponsitory,ChanglePassWordValidation validation) 
        {

            this.userReponsitory = userReponsitory;
            this.validation = validation;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                var user = await userReponsitory.GetUser();
                return Ok(BaseReponsitory<UserDTO>.WithData(user, 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost("ChanglePassword")]
        [Authorize]
        public async Task<IActionResult> ChanglePassword(ChanglePassWordModel model)
        {
            try
            {
                ValidationResult result=validation.Validate(model);
                if (!result.IsValid)
                {
                    return BadRequest(BaseReponsitory<Dictionary<string, string>>.WithMessage(result.Errors.ToDictionary(e => e.PropertyName, e => e.ErrorMessage),400));
                    
                }
                await userReponsitory.ChanglePassWord(model);
                return Ok(BaseReponsitory<string>.WithMessage("Đổi mật khẩu thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost("UpdateAvatar")]
        [Authorize]
        public async Task<IActionResult> UpdateAvatar( IFormFile Avatar)
        {
            try
            {
                await userReponsitory.UpdateImage(Avatar);
                return Ok(BaseReponsitory<string>.WithMessage("Đổi  ảnh đại diện thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost("DeleteAvatar")]
        [Authorize]
        public async Task<IActionResult> DeleteAvatr()
        {
            try
            {
                await userReponsitory.DeleteImage();
                return Ok(BaseReponsitory<string>.WithMessage("Xóa ảnh đại diện thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
