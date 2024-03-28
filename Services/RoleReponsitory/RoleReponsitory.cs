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
        
        private readonly IClaimService claimService;
        private readonly RoleManager<Role> roleManager;

        public RoleReponsitory(IClaimService claimService,RoleManager<Role> roleManager)
        {
            
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
            var role = await roleManager.FindByIdAsync(Id);

            if (role != null)
            {
                var result = await roleManager.DeleteAsync(role);


            }
        }

        public async Task<List<RoleDTO>> GetAll()
        {
            var role = await roleManager.Roles.ToListAsync();
            return role.OrderByDescending(x => x.Create_at).Select(e => new RoleDTO
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                Create_at = e.Create_at,
            }).ToList();
        }

        public async Task<RoleDetailDTO> GetById(string Id)
        {
            var role =await roleManager.FindByIdAsync(Id);
            if (role == null)
            {
                return null;
            }
            var existingClaims = await roleManager.GetClaimsAsync(role);
            return new RoleDetailDTO
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description,
                Create_at = role.Create_at,
                claimSubject=new ClaimSubject
                {
                    IsView= existingClaims.Any(ex => ex.Type == "subject" && ex.Value == "view"),
                    IsEdit= existingClaims.Any(ex => ex.Type == "subject" && ex.Value == "edit"),
                },
                claimDocument=new ClaimDocument
                {
                    IsView= existingClaims.Any(ex => ex.Type == "document" && ex.Value == "view"),
                    IsEdit = existingClaims.Any(ex => ex.Type == "document" && ex.Value == "edit"),
                    IsAdd = existingClaims.Any(ex => ex.Type == "document" && ex.Value == "add"),
                    IsCreate = existingClaims.Any(ex => ex.Type == "document" && ex.Value == "create"),
                    IsDelete = existingClaims.Any(ex => ex.Type == "document" && ex.Value == "delete"),
                    IsDownload= existingClaims.Any(ex => ex.Type == "document" && ex.Value == "download"),
                },
                claimPrivateFile=new ClaimPrivateFile
                {
                    IsView = existingClaims.Any(ex => ex.Type == "private" && ex.Value == "view"),
                    IsEdit = existingClaims.Any(ex => ex.Type == "private" && ex.Value == "edit"),
                    IsCreate = existingClaims.Any(ex => ex.Type == "private" && ex.Value == "create"),
                    IsDelete = existingClaims.Any(ex => ex.Type == "private" && ex.Value == "delete"),
                    IsDownload = existingClaims.Any(ex => ex.Type == "private" && ex.Value == "download"),
                },
                claimExam=new ClaimExam
                {
                    IsView = existingClaims.Any(ex => ex.Type == "exam" && ex.Value == "view"),
                    IsEdit = existingClaims.Any(ex => ex.Type == "exam" && ex.Value == "edit"),
                    IsCreate = existingClaims.Any(ex => ex.Type == "exam" && ex.Value == "create"),
                    IsDelete = existingClaims.Any(ex => ex.Type == "exam" && ex.Value == "delete"),
                    IsDownload = existingClaims.Any(ex => ex.Type == "exam" && ex.Value == "download"),
                    IsApprove= existingClaims.Any(ex => ex.Type == "exam" && ex.Value == "approve"),
                },
                claimNotification=new ClaimNotification
                {
                    IsView = existingClaims.Any(ex => ex.Type == "notification" && ex.Value == "view"),
                    IsEdit = existingClaims.Any(ex => ex.Type == "notification" && ex.Value == "edit"),
                    IsSystem= existingClaims.Any(ex => ex.Type == "notification" && ex.Value == "system"),
                    IsDelete = existingClaims.Any(ex => ex.Type == "notification" && ex.Value == "delete")               
                },
                claimRole=new ClaimRole
                {
                    IsView = existingClaims.Any(ex => ex.Type == "role" && ex.Value == "view"),
                    IsEdit = existingClaims.Any(ex => ex.Type == "role" && ex.Value == "edit"),
                    IsDelete = existingClaims.Any(ex => ex.Type == "role" && ex.Value == "delete"),
                    IsCreate = existingClaims.Any(ex => ex.Type == "role" && ex.Value == "create"),
                },
                claimAccount=new ClaimAccount
                {
                    IsView = existingClaims.Any(ex => ex.Type == "account" && ex.Value == "view"),
                    IsEdit = existingClaims.Any(ex => ex.Type == "account" && ex.Value == "edit"),
                    
                }
                
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
