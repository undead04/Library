﻿using Library.DTO;
using Library.Model;
using Library.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolLibrary.DTO;

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
        public async Task<IActionResult> getAlll()
        {
            try
            {
                var role=await roleReponsitory.GetAll();
                return Ok(BaseReponsitory<List<RoleDTO>>.WithData(role, 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> getById(int Id)
        {
            try
            {
                var role = await roleReponsitory.GetById(Id);
                if(role==null)
                {
                    return NotFound();
                }
                return Ok(BaseReponsitory<RoleDTO>.WithData(role, 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateRole(int Id,RoleModel model)
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
        public async Task<IActionResult> DeleteRole(int Id)
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
