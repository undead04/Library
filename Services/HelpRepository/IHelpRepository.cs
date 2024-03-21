using Library.DTO;
using Library.Model;

namespace Library.Services.HelpRepository
{
    public interface IHelpRepository
    {
        Task CreateHelp(HelpModel model);
        Task<List<HelpDTO>> GetAllHelp();
        Task<HelpDTO> GetHelpById(int Id);
    }
}
