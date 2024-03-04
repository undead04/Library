using Library.Data;
using Microsoft.AspNetCore.Identity;
using SchoolLibrary.DTO;
using SchoolLibrary.Model;
using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;

namespace SchoolLibrary.Server
{
    public class UserReponsitory : IUserReponsitory
    {
        private readonly MyDB context;
        private UserManager<ApplicationUser> userManager;
        private readonly ClaimsPrincipal _user;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserReponsitory(MyDB context, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor,SignInManager<ApplicationUser> signInManager,RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            this.userManager = userManager;
            _user = httpContextAccessor.HttpContext!.User;
            this.signInManager = signInManager;
            this.roleManager = roleManager;

        }
        public async Task<UserDTO> GetUser()
        {
            var email = _user.FindFirst(ClaimTypes.Email)!.Value;
            var user = await userManager.FindByEmailAsync(email);

            return new UserDTO
            {
                Email = email,
                UserName = user.UserName,
                CodeUser = user.MaUser,
                Sex = user.Sex,
                Phone = user.PhoneNumber,
                Address = user.Address,
              
                Avatar = user.Avatar==null?string.Empty:Convert.ToBase64String(user.Avatar),
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
            if(isEqual)
            {
                await userManager.ChangePasswordAsync(user, model.presentPassWord, model.newPassWord);
                await signInManager.RefreshSignInAsync(user);
            }

        }

        public async Task DeleteImage()
        {
            var user =await userManager.FindByEmailAsync(_user.FindFirst(ClaimTypes.Email)!.Value);
            if(user.Avatar!= null)
            {
                user.Avatar = null;
                await userManager.UpdateAsync(user);
            }
        }
    }
}
