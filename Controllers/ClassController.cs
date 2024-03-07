using Library.DTO;
using Library.Model;
using Library.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolLibrary.DTO;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IClassReponsitory classReponsitory;

        public ClassController(IClassReponsitory classReponsitory) 
        {
            this.classReponsitory = classReponsitory;
        }
        [HttpPost]
        public async Task<IActionResult> CreatClassRoom(ClassRoomModel model)
        {
            try
            {
                await classReponsitory.CreateClass(model);
                return Ok(BaseReponsitory<string>.WithMessage("Tạo lớp thành công",200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult>UpdateClass (int Id,ClassRoomModel model)
        {
            try
            {
                var classRoom =await classReponsitory.GetById(Id);
                if(classRoom == null)
                {
                    return NotFound();
                }
                await classReponsitory.UpdateClass(Id,model);
                return Ok(BaseReponsitory<string>.WithMessage("Cập nhật lớp thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteClass(int Id)
        {
            try
            {
                var classRoom =await classReponsitory.GetById(Id);
                if (classRoom == null)
                {
                    return NotFound();
                }
                await classReponsitory.DeleteClass(Id);
                return Ok(BaseReponsitory<string>.WithMessage("Xoá lớp thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> getByIdClass(int Id)
        {
            try
            {
                var classRoom =await classReponsitory.GetById(Id);
                if (classRoom == null)
                {
                    return NotFound();
                }
                return Ok(BaseReponsitory<ClassRoomDTO>.WithData(classRoom, 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllClass()
        {
            try
            {
                var classRoom = await classReponsitory.GetAllClass();
                
                return Ok(BaseReponsitory<List<ClassRoomDTO>>.WithData(classRoom, 200));
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
