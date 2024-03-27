using Library.Data;
using Library.DTO;
using Library.Model;
using Library.Services.ClaimsService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Library.Services.RoleReponsitory
{
    public class RoleReponsitory : IRoleReponsitory
    {
        private readonly MyDB context;
        private readonly IClaimService claimService;
        private readonly RoleManager<Role> roleManager;

        public RoleReponsitory(MyDB context,IClaimService claimService,RoleManager<Role> roleManager)
        {
            this.context = context;
            this.claimService = claimService;
            this.roleManager = roleManager;
        }
        public async Task CreateRole(RoleModel model)
        {
            var role = new Role
            {
                Name = model.Name,
                Description = model.Description,
            };
            var result = await roleManager.CreateAsync(role);
            if(result.Succeeded)
            {
                if (model.claimSubject!.IsView)
                {
                    await claimService.CreateClaims(role, "subject", "view");
                }
                if (model.claimSubject!.IsEdit)
                {
                    await claimService.CreateClaims(role, "subject", "edit");
                }
                if (model.claimPrivateFile!.IsCreate)
                {
                    await claimService.CreateClaims(role, "private", "create");
                }
                if (model.claimPrivateFile!.IsView)
                {
                    await claimService.CreateClaims(role, "private", "view");
                }
                if (model.claimPrivateFile!.IsEdit)
                {
                    await claimService.CreateClaims(role, "private", "edit");
                }
                if (model.claimPrivateFile!.IsDelete)
                {
                    await claimService.CreateClaims(role, "private", "delete");
                }
                if (model.claimPrivateFile!.IsDownload)
                {
                    await claimService.CreateClaims(role, "private", "download");
                }
                if (model.claimDocument!.IsCreate)
                {
                    await claimService.CreateClaims(role, "document", "create");
                }
                if (model.claimDocument!.IsView)
                {
                    await claimService.CreateClaims(role, "document", "view");
                }
                if (model.claimDocument!.IsEdit)
                {
                    await claimService.CreateClaims(role, "document", "edit");
                }
                if (model.claimDocument!.IsDelete)
                {
                    await claimService.CreateClaims(role, "document", "delete");
                }
                if (model.claimDocument!.IsDownload)
                {
                    await claimService.CreateClaims(role, "document", "download");
                }
                if (model.claimDocument!.IsAdd)
                {
                    await claimService.CreateClaims(role, "document", "add");
                }
                if (model.claimExam!.IsCreate)
                {
                    await claimService.CreateClaims(role, "exam", "create");
                }
                if (model.claimExam!.IsView)
                {
                    await claimService.CreateClaims(role, "exam", "view");
                }
                if (model.claimExam!.IsEdit)
                {
                    await claimService.CreateClaims(role, "exam", "edit");
                }
                if (model.claimExam!.IsDelete)
                {
                    await claimService.CreateClaims(role, "exam", "delete");
                }
                if (model.claimExam!.IsDownload)
                {
                    await claimService.CreateClaims(role, "exam", "download");
                }
                if (model.claimExam!.IsApprove)
                {
                    await claimService.CreateClaims(role, "exam", "approve");
                }
                if (model.claimNotification!.IsView)
                {
                    await claimService.CreateClaims(role, "notification", "view");
                }
                if (model.claimNotification!.IsEdit)
                {
                    await claimService.CreateClaims(role, "notification", "edit");
                }
                if (model.claimNotification!.IsDelete)
                {
                    await claimService.CreateClaims(role, "notification", "delete");
                }
                if (model.claimNotification!.IsSystem)
                {
                    await claimService.CreateClaims(role, "notification", "system");
                }
                if (model.claimRole!.IsCreate)
                {
                    await claimService.CreateClaims(role, "role", "create");
                }
                if (model.claimRole!.IsView)
                {
                    await claimService.CreateClaims(role, "role", "view");
                }
                if (model.claimRole!.IsEdit)
                {
                    await claimService.CreateClaims(role, "role", "edit");
                }
                if (model.claimRole!.IsDelete)
                {
                    await claimService.CreateClaims(role, "role", "delete");
                }
                if (model.claimAccount!.IsView)
                {
                    await claimService.CreateClaims(role, "account", "view");
                }
                if (model.claimAccount!.IsEdit)
                {
                    await claimService.CreateClaims(role, "account", "edit");
                }
            }

        }

        public async Task DeleteRole(string Id)
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
            return role.OrderByDescending(x => x.Create_at).Select(e => new RoleDTO
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                Create_at = e.Create_at,
            }).ToList();
        }

        public async Task<RoleDTO> GetById(string Id)
        {
            var role = await context.roles.FirstOrDefaultAsync(x => x.Id == Id);
            if (role == null)
            {
                return null;
            }
            var existingClaims = await roleManager.GetClaimsAsync(role);
            return new RoleDTO
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description,
                Create_at = role.Create_at
            };
        }

        public async Task UpdateRole(string Id, RoleModel model)
        {
            var role = await roleManager.FindByIdAsync(Id);
            if (role != null)
            {
                role.Create_at = DateTime.Now;
                role.Name = model.Name;
                role.Description = model.Description;
                await claimService.DeleteClaims(role);
                if (model.claimSubject!.IsView)
                {
                    await claimService.CreateClaims(role, "subject", "view");
                }
                if (model.claimSubject!.IsEdit)
                {
                    await claimService.CreateClaims(role, "subject", "edit");
                }
                if (model.claimPrivateFile!.IsCreate)
                {
                    await claimService.CreateClaims(role, "private", "create");
                }
                if (model.claimPrivateFile!.IsView)
                {
                    await claimService.CreateClaims(role, "private", "view");
                }
                if (model.claimPrivateFile!.IsEdit)
                {
                    await claimService.CreateClaims(role, "private", "edit");
                }
                if (model.claimPrivateFile!.IsDelete)
                {
                    await claimService.CreateClaims(role, "private", "delete");
                }
                if (model.claimPrivateFile!.IsDownload)
                {
                    await claimService.CreateClaims(role, "private", "download");
                }
                if (model.claimDocument!.IsCreate)
                {
                    await claimService.CreateClaims(role, "document", "create");
                }
                if (model.claimDocument!.IsView)
                {
                    await claimService.CreateClaims(role, "document", "view");
                }
                if (model.claimDocument!.IsEdit)
                {
                    await claimService.CreateClaims(role, "document", "edit");
                }
                if (model.claimDocument!.IsDelete)
                {
                    await claimService.CreateClaims(role, "document", "delete");
                }
                if (model.claimDocument!.IsDownload)
                {
                    await claimService.CreateClaims(role, "document", "download");
                }
                if (model.claimDocument!.IsAdd)
                {
                    await claimService.CreateClaims(role, "document", "add");
                }
                if (model.claimExam!.IsCreate)
                {
                    await claimService.CreateClaims(role, "exam", "create");
                }
                if (model.claimExam!.IsView)
                {
                    await claimService.CreateClaims(role, "exam", "view");
                }
                if (model.claimExam!.IsEdit)
                {
                    await claimService.CreateClaims(role, "exam", "edit");
                }
                if (model.claimExam!.IsDelete)
                {
                    await claimService.CreateClaims(role, "exam", "delete");
                }
                if (model.claimExam!.IsDownload)
                {
                    await claimService.CreateClaims(role, "exam", "download");
                }
                if (model.claimExam!.IsApprove)
                {
                    await claimService.CreateClaims(role, "exam", "approve");
                }
                if (model.claimNotification!.IsView)
                {
                    await claimService.CreateClaims(role, "notification", "view");
                }
                if (model.claimNotification!.IsEdit)
                {
                    await claimService.CreateClaims(role, "notification", "edit");
                }
                if (model.claimNotification!.IsDelete)
                {
                    await claimService.CreateClaims(role, "notification", "delete");
                }
                if (model.claimNotification!.IsSystem)
                {
                    await claimService.CreateClaims(role, "notification", "system");
                }
                if (model.claimRole!.IsCreate)
                {
                    await claimService.CreateClaims(role, "role", "create");
                }
                if (model.claimRole!.IsView)
                {
                    await claimService.CreateClaims(role, "role", "view");
                }
                if (model.claimRole!.IsEdit)
                {
                    await claimService.CreateClaims(role, "role", "edit");
                }
                if (model.claimRole!.IsDelete)
                {
                    await claimService.CreateClaims(role, "role", "delete");
                }
                if (model.claimAccount!.IsView)
                {
                    await claimService.CreateClaims(role, "account", "view");
                }
                if (model.claimAccount!.IsEdit)
                {
                    await claimService.CreateClaims(role, "account", "edit");
                }
                await roleManager.UpdateAsync(role);
            }
        }
    }
}
