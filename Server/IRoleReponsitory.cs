using Library.DTO;
using Library.Model;

namespace Library.Server
{
    public interface IRoleReponsitory
    {
        Task CreateRole(RoleModel model);
        Task<List<RoleDTO>> GetAll();
        Task<RoleDTO> GetById(int Id);
        Task UpdateRole(int Id, RoleModel model);
        Task DeleteRole(int Id);
    }
}
