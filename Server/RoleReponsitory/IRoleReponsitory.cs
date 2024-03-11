using Library.DTO;
using Library.Model;

namespace Library.Server.RoleReponsitory
{
    public interface IRoleReponsitory
    {
        Task CreateRole(RoleModel model);
        Task<List<RoleDTO>> GetAll();
        Task<RoleDTO> GetById(string Id);
        Task UpdateRole(string Id, RoleModel model);
        Task DeleteRole(string Id);
    }
}
