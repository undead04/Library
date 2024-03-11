using Library.DTO;
using Library.Model;
using Library.Server.LessonReponsitory;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly ILessonReponsitory reponsitory;

        public LessonController(ILessonReponsitory reponsitory)
        {
            this.reponsitory = reponsitory;
        }
        [HttpPost]
        public async Task<IActionResult> CreateLesson([FromForm]LessonModel model)
        {
            try
            {
                await reponsitory.CreateLesson(model);
                return Ok(BaseReponsitory<string>.WithMessage("Tạo bài học thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("topic/{Id}")]
        public async Task<IActionResult> GetAllLesson(int Id)
        {
            try
            {
               var lesson=  await reponsitory.GetAllLesson(Id);
                return Ok(BaseReponsitory<List<LessonDTO>>.WithData(lesson, 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> getLessonById(int Id)
        {
            try
            {
                var lesson = await reponsitory.GetLessonById(Id);
                return Ok(BaseReponsitory<LessonDTO>.WithData(lesson, 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteLesson(int Id)
        {
            try
            {
                await reponsitory.DeleteLesson(Id);
                return Ok(BaseReponsitory<string>.WithMessage("Xóa bài học thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateLesson(int Id,LessonModel model)
        {
            try
            {
                await reponsitory.UpdateLesson(Id,model);
                return Ok(BaseReponsitory<string>.WithMessage("Cập nhật bài học thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost("addDocument")]
        public async Task<IActionResult> AddDocumentLesson(AddDocumentLessonModel model)
        {
            try
            {
                await reponsitory.AddDocumentLesson(model);
                return Ok(BaseReponsitory<string>.WithMessage("Thêm bài giảng thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
