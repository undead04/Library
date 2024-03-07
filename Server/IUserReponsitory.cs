﻿using Library.Model;
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
        Task<List<UserDTO>> GetAllUser();
        Task DeleteUser(string id);
        Task<UserDTO> GetUserById(string id);
        Task CreateUser(SingnUpModel model);
        Task UpdateUser(string Id,SingnUpModel model);
        Task<List<UserDTO>> Search(string? search, int[]? RoleId);

        

    }
}
