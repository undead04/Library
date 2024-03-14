using Library.Data;
using Library.DTO;
using Library.Model;
using Library.Services.ReissuePassword;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Utilities;
using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;

namespace Library.Services.UserReponsitory
{
    public class UserReponsitory : IUserReponsitory
    {
        private readonly MyDB context;
        private UserManager<ApplicationUser> userManager;
        private readonly ClaimsPrincipal _user;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IReissuePassword reissuePassword;

        public UserReponsitory(MyDB context, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, IReissuePassword reissuePassword)
        {
            this.context = context;
            this.userManager = userManager;
            _user = httpContextAccessor.HttpContext!.User;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.reissuePassword = reissuePassword;

        }
        public async Task<UserDTO> GetUser()
        {
            var email = _user.FindFirst(ClaimTypes.Email)!.Value;
            var user = await userManager.FindByEmailAsync(email);

            return new UserDTO
            {
                Email = email,
                UserName = user.UserName,
                CodeUser = user.UserCode,
                Sex = user.Sex,
                Phone = user.PhoneNumber,
                Address = user.Address,
                Avatar = user.Avatar == null ? string.Empty : Convert.ToBase64String(user.Avatar),
            };
        }

        public async Task UpdateImage(IFormFile Avatar)
        {
            var email = _user.FindFirst(ClaimTypes.Email).Value;
            var user = await userManager.FindByEmailAsync(email);
            using (MemoryStream memory = new MemoryStream())
            {
                await Avatar.CopyToAsync(memory);
                byte[] fileBytes = memory.ToArray();
                user.Avatar = fileBytes;
                await userManager.UpdateAsync(user);
            }
        }
        public async Task ChanglePassWord(ChanglePassWordModel model)
        {
            var email = _user.FindFirst(ClaimTypes.Email)!.Value;
            var user = await userManager.FindByEmailAsync(email);
            bool isEqual = model.comformPassword == model.newPassWord;
            if (isEqual)
            {
                await userManager.ChangePasswordAsync(user, model.presentPassWord, model.newPassWord);
                await signInManager.RefreshSignInAsync(user);
            }

        }

        public async Task DeleteImage()
        {
            var user = await userManager.FindByEmailAsync(_user.FindFirst(ClaimTypes.Email)!.Value);
            if (user.Avatar != null)
            {
                user.Avatar = null;
                await userManager.UpdateAsync(user);
            }
        }
        public async Task<List<UserDTO>> GetAllUser()
        {
            var users = await userManager.Users.ToListAsync();
            return users.Select(user => new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                CodeUser = user.UserCode,
                Sex = user.Sex,
                Phone = user.PhoneNumber,
                Address = user.Address,
                Avatar = user.Avatar == null ? string.Empty : Convert.ToBase64String(user.Avatar),
            }).ToList();
        }
        public async Task DeleteUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                await userManager.DeleteAsync(user);
            }
        }
        public async Task<UserDTO> GetUserById(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return null;
            }
            return new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                CodeUser = user.UserCode,
                Sex = user.Sex,
                Phone = user.PhoneNumber,
                Address = user.Address,
                Avatar = user.Avatar == null ? string.Empty : Convert.ToBase64String(user.Avatar),
            };
        }
        public async Task CreateUser(SingnUpModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                Sex = model.Sex,
                UserCode = model.UserCode,
                Address = model.Address,
                PhoneNumber=model.Phone
            };
            string password = reissuePassword.CreatPassword();
            var result = await userManager.CreateAsync(user, password);
            var role = await roleManager.FindByIdAsync(model.RoleId);
            if (result.Succeeded)
            {
                if (await roleManager.RoleExistsAsync(role.Name))
                {
                    await userManager.AddToRoleAsync(user, role.Name);
                }
                switch (role.Name)
                {
                    case AppRole.Student:
                        var student = new Student
                        {
                            UserId = user.Id,
                            ClassRoomId = model.ClassRoomId,
                            MajorId = model.MajorsId,
                        };
                        await context.students.AddAsync(student);
                        await context.SaveChangesAsync();
                        break;
                    case AppRole.Teacher:
                        var teacher = new Tearcher
                        {
                            UserId = user.Id,
                            MajorId = model.MajorsId,
                        };
                        await context.tearchers.AddAsync(teacher);
                        await context.SaveChangesAsync();
                        break;
                }

            }
            


        }
        public async Task UpdateUser(string Id, SingnUpModel model)
        {
            var user = await userManager.FindByIdAsync(Id);
            if (user != null)
            {
                //user.RoleId = model.RoleId;
                await userManager.UpdateAsync(user);
            }


        }
        public async Task<List<UserDTO>> Search(string search, int[] RoleId)
        {
            var user = await userManager.Users.ToListAsync();
            if (!string.IsNullOrEmpty(search))
            {
                user = user.Where(us => us.UserCode.Contains(search) || us.Email.Contains(search) || us.UserName.Contains(search)).ToList();
            }
            if (RoleId != null)
            {

            }
            return user.Select(user => new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                CodeUser = user.UserCode,
                Sex = user.Sex,
                Phone = user.PhoneNumber,
                Address = user.Address,
            }).ToList();
        }
        public async Task<List<SubjectDTO>> GetTearcherSubject(string tearcherId)
        {
            var subjects=await context.subjects.Where(su=>su.subjectClassRooms.Any(su=>su.TearcherId==tearcherId)).ToListAsync();
            return subjects.Select(su => new SubjectDTO
            {
                Id = su.Id,
                Name = su.Name,
                SubjectCode = su.SubjectCode,
                Describe = su.Describe,

            }).ToList();
        }
    }
}
