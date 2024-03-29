using Library.Model;
using Library.Model.DTO;
using Library.Repository.RoleReponsitory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleReponsitory roleReponsitory;

        public RoleController(IRoleReponsitory roleReponsitory)
        { 
            this.roleReponsitory=roleReponsitory;
        }
        [HttpPost]
        [Authorize(Policy = "RoleCreate")]
        public async Task<IActionResult> CreateRole(RoleModel model)
        {
            try
            {
                await roleReponsitory.CreateRole(model);
                return Ok(BaseReponsitory<string>.WithMessage("Tạo role thành công", 201));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet]
        
        public async Task<IActionResult> getAlll(string? search)
        {
            try
            {
                var role=await roleReponsitory.GetAll(search);
                return Ok(BaseReponsitory<List<RoleDTO>>.WithData(role, 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{Id}")]
        [Authorize(Policy = "RoleView")]
        public async Task<IActionResult> getById(string Id)
        {
            try
            {
                var role = await roleReponsitory.GetById(Id);
                if(role==null)
                {
                    return NotFound();
                }
                return Ok(BaseReponsitory<RoleDetailDTO>.WithData(role, 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("{Id}")]
        [Authorize(Policy = "RoleEdit")]
        public async Task<IActionResult> UpdateRole(string Id,RoleModel model)
        {
            try
            {
                var role = await roleReponsitory.GetById(Id);
                if (role == null)
                {
                    return NotFound();
                }
                await roleReponsitory.UpdateRole(Id, model);
                return Ok(BaseReponsitory<string>.WithMessage("Cập nhật vai trò thành công",200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("{Id}")]
        [Authorize(Policy = "RoleDelete")]
        public async Task<IActionResult> DeleteRole(string Id)
        {
            try
            {
                var role = await roleReponsitory.GetById(Id);
                if (role == null)
                {
                    return NotFound();
                }
                await roleReponsitory.DeleteRole(Id);
                return Ok(BaseReponsitory<string>.WithMessage("Xóa vai trò thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
        }
       

    }
}
