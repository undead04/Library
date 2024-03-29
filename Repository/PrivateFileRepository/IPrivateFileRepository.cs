using Library.Model;
using Library.Model.DTO;

namespace Library.Repository.PrivateFileRepository
{
    public interface IPrivateFileRepository
    {
        Task UploadPrivateFile(PrivateFileModel model);
        Task DeletePrivateFile(int id);
        Task RenamePrivateFile(int id, string name);
        Task<List<PrivateFileDTO>> GetAll();
        Task<PrivateFileDTO> GetById(int id);
    }
}
