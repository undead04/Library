using FluentValidation.Results;
using Library.Model;
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

        public UserController(IUserReponsitory userReponsitory, ChanglePassWordValidation validation)
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
                ValidationResult result = validation.Validate(model);
                if (!result.IsValid)
                {
                    return BadRequest(BaseReponsitory<Dictionary<string, string>>.WithMessage(result.Errors.ToDictionary(e => e.PropertyName, e => e.ErrorMessage), 400));

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
        public async Task<IActionResult> UpdateAvatar(IFormFile Avatar)
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
        [HttpGet("All")]
        public async Task<IActionResult> GetAllUser()
        {
            try
            {
                var user = await userReponsitory.GetAllUser();
                return Ok(BaseReponsitory<List<UserDTO>>.WithData(user, 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetByIdlUser(string Id)
        {
            try
            {
                var user = await userReponsitory.GetUserById(Id);
                return Ok(BaseReponsitory<UserDTO>.WithData(user, 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteUser(string Id)
        {
            try
            {
                var user = await userReponsitory.GetUserById(Id);
                if (user == null)
                {
                    return NotFound();
                }
                await userReponsitory.DeleteUser(Id);
                return Ok(BaseReponsitory<string>.WithMessage("Xóa người dùng thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateUser(string Id, SingnUpModel model)
        {
            try
            {
                var user = await userReponsitory.GetUserById(Id);
                if (user == null)
                {
                    return NotFound();
                }
                await userReponsitory.UpdateUser(Id, model);
                return Ok(BaseReponsitory<string>.WithMessage("Cập nhật người dùng thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(SingnUpModel model)
        {
            try
            {

                await userReponsitory.CreateUser(model);
                return Ok(BaseReponsitory<string>.WithMessage("Thêm người dùng thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("FilterSearch")]
        public async Task<IActionResult> FilterSearch(string? search, int[]? RoleId)
        {
            try
            {

                var user= await userReponsitory.Search(search,RoleId);
                return Ok(BaseReponsitory<List<UserDTO>>.WithData(user, 200));
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
