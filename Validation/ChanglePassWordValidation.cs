using FluentValidation;
using Library.Data;
using Microsoft.AspNetCore.Identity;

using SchoolLibrary.Model;
using System.Security.Claims;

namespace SchoolLibrary.Validation
{
    public class ChanglePassWordValidation:AbstractValidator<ChanglePassWordModel>
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ClaimsPrincipal _user;
        public ChanglePassWordValidation(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            this.userManager = userManager;
            _user = httpContextAccessor.HttpContext!.User;
            RuleFor(x => x.presentPassWord).Cascade(CascadeMode.Stop).Must(IsPresentPassWord).WithMessage("Nhập không đúng mật khẩu")
                .NotEmpty().WithMessage("Không được trống");
            RuleFor(x => x.newPassWord).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Không được để trống")
               .Must(CheckLengthPasswor).WithMessage("Phải có 8 ký tự trở lên")
               .Must(CheckUpperPasswor).WithMessage("Phải có ít nhất 1 ký tự viết hoa")
               .Must(CheckLowerPasswor).WithMessage("Phải có ít nhất 1 ký tự từ a-z")
               .Must(CheckDigitPasswor).WithMessage("Phải có ít nhất 1 ký tự là số")
               .Must(CheckSpecialPasswor).WithMessage("Phải có ít nhất 1 ký tự đặt biệt")
               .Custom((Password, context) =>
               {
                   var model = (ChanglePassWordModel)context.InstanceToValidate;
                   if (!CheckPasswordComform(model))
                   {
                       context.AddFailure("PasswordComfrom không đúng");
                   }

               });

        }


        private bool CheckPasswordComform(ChanglePassWordModel model)
        {
            return model.newPassWord == model.comformPassword;
        }
        private bool IsPresentPassWord(string passWord)
        {
            var email=_user.FindFirst(ClaimTypes.Email)!.Value;
            var user = userManager.FindByEmailAsync(email).Result;
            var passwordValid =  userManager.CheckPasswordAsync(user, passWord).Result;
            if (passwordValid)
            {
                return true;
            }
            return false;

        }
        
        
        private bool CheckUpperPasswor(string password)
        {
            foreach (char c in password)
            {
                if (char.IsUpper(c))
                {
                    return true;
                }
            }
            return false;
        }
        private bool CheckLengthPasswor(string password)
        {

            return password.Length >= 8 ? true : false;
        }
        private bool CheckLowerPasswor(string password)
        {
            foreach (char c in password)
            {
                if (char.IsLower(c))
                {
                    return true;
                }
            }
            return false;
        }
        private bool CheckDigitPasswor(string password)
        {
            foreach (char c in password)
            {
                if (char.IsDigit(c))
                {
                    return true;
                }
            }
            return false;
        }
        private bool CheckSpecialPasswor(string password)
        {
            foreach (char c in password)
            {
                if (!char.IsLetterOrDigit(c))
                {
                    return true;
                }
            }
            return false;
        }
    }
}

