using Library.Model;
using Library.Model.DTO;

namespace Library.Repository.MajorReposntiory
{
    public interface IMajorReponsitory
    {
        Task CreateMajor(MajorModel model);
        Task UpdateMajor(int Id, MajorModel model);
        Task DeleteMajor(int Id);
        Task<MajorDTO> GetMajor(int Id);
        Task<List<MajorDTO>> GetAllMajor();
    }
}
