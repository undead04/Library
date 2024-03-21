using Library.Model;
using Library.Services.PrivateFileRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Library.DTO;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrivateFileController : ControllerBase
    {
        private readonly IPrivateFileRepository privateFileRepository;

        public PrivateFileController(IPrivateFileRepository privateFileRepository) 
        {
            this.privateFileRepository=privateFileRepository;


        }
        [HttpPost]
        public async Task<IActionResult> UploadFile([FromForm]PrivateFileModel model)
        {
            try
            {
                await privateFileRepository.UploadPrivateFile(model);
                return Ok(BaseReponsitory<string>.WithMessage("Tạo thành công", 200));

            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPrivateFile()
        {
            try
            {
                var  privateFile= await privateFileRepository.GetAll();
                return Ok(BaseReponsitory<List<PrivateFileDTO>>.WithData(privateFile, 200));

            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrivateFile(int id)
        {
            try
            {
                
                var privateFile = await privateFileRepository.GetById(id);
                if(privateFile==null)
                {
                    return NotFound();
                }
                await privateFileRepository.DeletePrivateFile(id);
                return Ok(BaseReponsitory<string>.WithMessage("Xóa thành công", 200));

            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> RenamePrivateFile(int id,string name)
        {
            try
            {

                var privateFile = await privateFileRepository.GetById(id);
                if (privateFile == null)
                {
                    return NotFound();
                }
                await privateFileRepository.RenamePrivateFile(id,name);
                return Ok(BaseReponsitory<string>.WithMessage("đổi tên thành công", 200));

            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
