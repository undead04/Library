using Library.Model;
using Library.Model.DTO;
using Library.Repository.ClassLessonRepository;
using Library.Repository.LessonReponsitory;
using Library.Repository.ResourceReponsitory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly ILessonReponsitory reponsitory;
        private readonly IResourceReponsitory resourceReponsitory;
        private readonly IClassLessonRepository classLessonRepository;

        public LessonController(ILessonReponsitory reponsitory,IResourceReponsitory resourceReponsitory,IClassLessonRepository classLessonRepository)
        {
            this.reponsitory = reponsitory;
            this.resourceReponsitory=resourceReponsitory;
            this.classLessonRepository = classLessonRepository;
        }
        [HttpPost]
        [Authorize(Policy ="DocumentCreate")]
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
        [Authorize(Policy = "DocumentView")]
        public async Task<IActionResult> GetAllLesson(int Id,int?ClassId)
        {
            try
            {
               var lesson=  await reponsitory.GetAllLesson(Id,ClassId);
                return Ok(BaseReponsitory<List<LessonDTO>>.WithData(lesson, 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{Id}")]
        [Authorize(Policy = "DocumentView")]
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
        [Authorize(Policy = "DocumentDelete")]
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
        [Authorize(Policy = "DocumentEdit")]
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
        [Authorize(Policy = "DocumentCreate")]
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
        [HttpPost("CreateAllLesson")]
        [Authorize(Policy = "DocumentCreate")]
        public async Task<IActionResult> CreateAllLesson([FromForm]CreateAllLesson model)
        {
            try
            {
                var lessonModel = new LessonModel
                {
                    SubjectId=model.SubjectId,
                    Title=model.Title,
                    TopicId=model.TopicId,
                    File=model.Lesson,
                };
                var lessonId =await reponsitory.CreateLesson(lessonModel);
                var resourceModel = new ResourceModel
                {
                    LessonId=lessonId,
                    SubjectId=model.SubjectId,
                    File = model.Resource,
                };
                await resourceReponsitory.CreateResource(resourceModel);
                if(model.ClassId.Count()>0)
                {
                    int[] ArrayLessonId = new int[1];
                    ArrayLessonId[0] = lessonId;

                    var assignModel = new AssignDocumentModel
                    {
                        ClassId = model.ClassId,
                        LessonId = ArrayLessonId
                    };
                    await classLessonRepository.AssignDocuments(assignModel);
                }
                return Ok(BaseReponsitory<string>.WithMessage("Thêm bài giảng thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
