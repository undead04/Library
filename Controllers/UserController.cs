using FluentValidation.Results;
using Library.DTO;
using Library.Model;
using Library.Services.JWTService;
using Library.Services.RoleReponsitory;
using Library.Services.UploadService;
using Library.Services.UserReponsitory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolLibrary.Validation;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserReponsitory userReponsitory;
        private readonly ChanglePassWordValidation validation;
        private readonly IJWTSevice jwtService;
        private readonly IUploadService uploadservice;
        private readonly IRoleReponsitory roleRepository;

        public UserController(IUserReponsitory userReponsitory, ChanglePassWordValidation validation,IJWTSevice jWTSevice,IUploadService uploadService,IRoleReponsitory roleReponsitory)
        {

            this.userReponsitory = userReponsitory;
            this.validation = validation;
            this.jwtService = jWTSevice;
            this.uploadservice = uploadService;
            this.roleRepository = roleReponsitory;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                var userId = await jwtService.ReadToken();
                var user = await userReponsitory.GetUserById(userId);
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
                var userId = await jwtService.ReadToken();
                await userReponsitory.ChanglePassWord(model,userId);
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
                var userId=await jwtService.ReadToken();
                await userReponsitory.UpdateImage(Avatar, userId);
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
                var userId= await jwtService.ReadToken();
                await userReponsitory.DeleteImage(userId);
                return Ok(BaseReponsitory<string>.WithMessage("Xóa ảnh đại diện thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("All")]
        public async Task<IActionResult> GetAllUser(string? search,string?roleId)
        {
            try
            {
                if(!string.IsNullOrEmpty(roleId))
                {
                    var role=roleRepository.GetById(roleId);
                    if(role==null)
                    {
                        return NotFound();
                    }
                }
                var user = await userReponsitory.GetAllUser(search,roleId);
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
        [HttpGet("tearchSubject/{Id}")]
        public async Task<IActionResult> GetAllTearcherSubject(string Id)
        {
            try
            {
                var subjects= await userReponsitory.GetTearcherSubject(Id);
                return Ok(BaseReponsitory<List<SubjectDTO>>.WithData(subjects, 200));
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
