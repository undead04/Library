using Library.Model;
using Library.Model.DTO;

namespace Library.Repository.HelpRepository
{
    public interface IHelpRepository
    {
        Task CreateHelp(HelpModel model);
        Task<List<HelpDTO>> GetAllHelp();
        Task<HelpDTO> GetHelpById(int Id);
    }
}
