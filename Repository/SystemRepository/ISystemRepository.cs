using Library.Model;
using Library.Model.DTO;

namespace Library.Repository.SystemRepository
{
    public interface ISystemRepository
    {
        Task CreateSystem(SystemModel model);
        Task<SystemDTO> GetSystem();
        Task UpdateSystem(SystemModel model);
    }
}
