using Library.DTO;
using Library.Model;

namespace Library.Server.MajorReposntiory
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
