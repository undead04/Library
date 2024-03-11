using Library.DTO;
using Library.Model;
using Library.Server.MajorReposntiory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MajorController : ControllerBase
    {
        private readonly IMajorReponsitory reponsitory;

        public MajorController(IMajorReponsitory reponsitory) 
        {
            this.reponsitory = reponsitory;
        }
        [HttpPost]
        public async Task<IActionResult> CreateMajor(MajorModel model)
        {
            try
            {
                await reponsitory.CreateMajor(model);
                return Ok(BaseReponsitory<string>.WithMessage("Tạo khoa thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateMajor(int Id,MajorModel model)
        {
            try
            {
                var major = await reponsitory.GetMajor(Id);
                if(major==null)
                {
                    return NotFound();
                }
                await reponsitory.UpdateMajor(Id,model);
                return Ok(BaseReponsitory<string>.WithMessage("Cập nhật khoa thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteMajor(int Id)
        {
            try
            {
                var major = await reponsitory.GetMajor(Id);
                if (major == null)
                {
                    return NotFound();
                }
                await reponsitory.DeleteMajor(Id);
                return Ok(BaseReponsitory<string>.WithMessage("Xóa khoa thành công", 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetByIdMajor(int Id)
        {
            try
            {
                var major = await reponsitory.GetMajor(Id);
                if (major == null)
                {
                    return NotFound();
                }
                
                return Ok(BaseReponsitory<MajorDTO>.WithData(major, 200));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public async Task<IActionResult> getAllMajor()
        {
            try
            {
                var major = await reponsitory.GetAllMajor();
               

                return Ok(BaseReponsitory<List<MajorDTO>>.WithData(major, 200));
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
