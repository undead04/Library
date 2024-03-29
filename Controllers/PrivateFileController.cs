using Library.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Library.Services.UploadService;
using Library.Model.DTO;
using Library.Repository.PrivateFileRepository;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrivateFileController : ControllerBase
    {
        private readonly IPrivateFileRepository privateFileRepository;
        private readonly IUploadService uploadService;

        public PrivateFileController(IPrivateFileRepository privateFileRepository,IUploadService uploadService) 
        {
            this.privateFileRepository=privateFileRepository;
            this.uploadService = uploadService;

        }
        [HttpPost]
        [Authorize(Policy ="PrivateView")]
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
        [Authorize(Policy = "PrivateView")]
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
        [Authorize(Policy = "PrivateDelete")]
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
        [Authorize(Policy = "PrivateEdit")]
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
        [HttpGet("download/{Id}")]
        [Authorize(Policy = "PrivateDownload")]
        public async Task<IActionResult> DownloadPrivateFile(int Id)
        {
            try
            {
                var privateFile = await privateFileRepository.GetById(Id);
                if (privateFile == null)
                {
                    return NotFound();
                }
                var result = await uploadService.DownloadFile(privateFile.Name, "Private");
                return File(result.Item1, result.Item2, result.Item3);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
