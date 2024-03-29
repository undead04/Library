using Library.Model;
using Library.Model.DTO;

namespace Library.Repository.RoleReponsitory
{
    public interface IRoleReponsitory
    {
        Task CreateRole(RoleModel model);
        Task<List<RoleDTO>> GetAll(string? search);
        Task<RoleDetailDTO> GetById(string Id);
        Task UpdateRole(string Id, RoleModel model);
        Task DeleteRole(string Id);
    }
}
