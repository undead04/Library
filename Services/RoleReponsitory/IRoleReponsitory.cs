﻿using Library.DTO;
using Library.Model;

namespace Library.Services.RoleReponsitory
{
    public interface IRoleReponsitory
    {
        Task CreateRole(RoleModel model);
        Task<List<RoleDTO>> GetAll();
        Task<RoleDetailDTO> GetById(string Id);
        Task UpdateRole(string Id, RoleModel model);
        Task DeleteRole(string Id);
    }
}
