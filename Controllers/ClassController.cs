using Library.DTO;
using Library.Model;
using Library.Server.ClassReponsitory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


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
        [HttpPost("AddTeacher")]
        public async Task<IActionResult> AddTearchClass (AddTearcherClassRoom model)
        {
            try
            {
                await classReponsitory.AddSubjectClass(model);
                return Ok(BaseReponsitory<string>.WithMessage("Thêm thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
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
        [HttpGet("subject/{Id}")]
        public async Task<IActionResult> getAllClassSubject(int Id)
        {
            try
            {
                var classRoom = await classReponsitory.GetAllCLassRoomSubject(Id);

                return Ok(BaseReponsitory<List<ClassRoomDTO>>.WithData(classRoom, 200));
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
