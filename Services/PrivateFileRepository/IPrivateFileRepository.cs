using Library.DTO;
using Library.Model;

namespace Library.Services.PrivateFileRepository
{
    public interface IPrivateFileRepository
    {
        Task UploadPrivateFile(PrivateFileModel model);
        Task DeletePrivateFile(int id);
        Task RenamePrivateFile(int id,string name);
        Task<List<PrivateFileDTO>> GetAll();
        Task<PrivateFileDTO> GetById(int id);
    }
}
