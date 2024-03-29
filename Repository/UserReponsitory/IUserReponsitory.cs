using Library.Model;
using Library.Data;
using Library.Model.DTO;

namespace Library.Repository.UserReponsitory
{
    public interface IUserReponsitory
    {

        Task ChanglePassWord(ChanglePassWordModel model, string userId);
        Task UpdateImage(IFormFile Avatar, string userId);
        Task DeleteImage(string userId);
        Task<List<UserDTO>> GetAllUser(string? search, string? roleId);
        Task DeleteUser(string id);
        Task<UserDTO> GetUserById(string id);
        Task CreateUser(SingnUpModel model);
        Task UpdateUser(string Id, SingnUpModel model);





    }
}
