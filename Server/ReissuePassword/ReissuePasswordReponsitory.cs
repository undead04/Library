using Library.Data;
using Library.Model;
using Library.Server.MailService;
using Microsoft.AspNetCore.Identity;

namespace Library.Server.ReissuePassword
{
    public class ReissuePasswordReponsitory : IReissuePassword
    {
        private IMailService mailService;
        private readonly UserManager<ApplicationUser> userManager;

        public ReissuePasswordReponsitory(IMailService mailService, UserManager<ApplicationUser> userManager)
        {
            this.mailService = mailService;
            this.userManager = userManager;
        }

        public async Task ReissuePassword(ReissuePasswordModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            MailData mailData = new MailData();
            mailData.ReceiverEmail = model.Email;
            mailData.ReceiverName = user.UserName;
            mailData.Title = "Cấp lại mật khẩu";
            // mật khẩu đc cấp lại
            string newPassword = CreatPassword();
            mailData.Body = $"Chào {user.UserName} đây là mật khẩu mới của bạn {newPassword}";
            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var result = await userManager.ResetPasswordAsync(user, token, newPassword);
            if (result.Succeeded)
            {
                mailService.SendMail(mailData);
            }

        }
        public string CreatPassword()
        {
            Random random = new Random();
            string password = "";
            for (int i = 1; i <= 8; i++)
            {
                switch (i)
                {
                    case 1:
                    case 2:
                        int number = random.Next(65, 91);
                        password += (char)number;
                        break;
                    case 3:
                    case 4:
                        password += (char)random.Next(97, 123);
                        break;
                    case 5:
                    case 6:
                        password += (char)random.Next(35, 39);
                        break;
                    case 7:
                    case 8:
                        password += random.Next(1, 10);
                        break;
                }
            }
            return password;
        }
    }
}
