using Library.Data;
using Library.DTO;
using Library.Model;
using Microsoft.EntityFrameworkCore;

namespace Library.Server
{
    public class RoleReponsitory : IRoleReponsitory
    {
        private readonly MyDB context;

        public RoleReponsitory(MyDB context) 
        {
            this.context = context;
        }
        public async Task CreateRole(RoleModel model)
        {
            var role = new Role
            {
                Name = model.Name,
                Description = model.Description,
                Create_at = DateTime.Now,
                
            };
            await context.roles.AddAsync(role);
            await context.SaveChangesAsync();
            
        }

        public async Task DeleteRole(int Id)
        {
            var role = await context.roles.FirstOrDefaultAsync(ro => ro.Id == Id);
            if (role != null)
            {
                context.Remove(role);
               await context.SaveChangesAsync();
            }
        }

        public async Task<List<RoleDTO>> GetAll()
        {
            var role = await context.roles.ToListAsync();
            return role.OrderByDescending(x=>x.Create_at).Select(e => new RoleDTO
            {
                Id=e.Id,
                Name=e.Name,
                Description=e.Description,
                Create_at = e.Create_at,
            }).ToList();
        }

        public async Task<RoleDTO> GetById(int Id)
        {
            var role = await context.roles.FirstOrDefaultAsync(x=>x.Id==Id);
            if(role== null)
            {
                return null;
            }
            return new RoleDTO
            {
                Id=role.Id,
                Name = role.Name,
                Description = role.Description,
                Create_at = role.Create_at
            };
        }

        public async Task UpdateRole(int Id, RoleModel model)
        {
            var role = await context.roles.FirstOrDefaultAsync(ro => ro.Id == Id);
            if(role != null)
            {
                role.Create_at=DateTime.Now;
                role.Name=model.Name;
                role.Description = model.Description;
                await context.SaveChangesAsync();
            }
        }
    }
}
