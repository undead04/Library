using SchoolLibrary.DTO;
using SchoolLibrary.Model;

namespace SchoolLibrary.Server
{
    public interface IUserReponsitory
    {
        Task<UserDTO> GetUser();
        Task UpdateImage(IFormFile Avatar);
        Task ChanglePassWord(ChanglePassWordModel model);
        Task DeleteImage();

    }
}
