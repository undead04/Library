using Library.Data;
using Library.Model;
using Microsoft.AspNetCore.Identity;

namespace Library.Server
{
    public class ReissuePasswordReponsitory:IReissuePassword
    {
        private IMailService mailService;
        private readonly UserManager<ApplicationUser> userManager;

        public ReissuePasswordReponsitory(IMailService mailService,UserManager<ApplicationUser> userManager)
        {
            this.mailService=mailService;
            this.userManager = userManager;
        }

        public async Task ReissuePassword(ReissuePasswordModel model)
        {
           var user=await userManager.FindByEmailAsync(model.Email);
            MailData mailData = new MailData();
            mailData.ReceiverEmail = model.Email;
            mailData.ReceiverName = user.UserName;
            mailData.Title = "Cấp lại mật khẩu";
            // mật khẩu đc cấp lại
            string newPassword = "An300998@";
            mailData.Body=$"Chào {user.UserName} đây là mật khẩu mới của bạn {newPassword}";
            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var result = await userManager.ResetPasswordAsync(user, token, newPassword);
            if(result.Succeeded)
            {
                mailService.SendMail(mailData);
            }
            
        }
    }
}
