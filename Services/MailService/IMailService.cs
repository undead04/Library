using Library.Model;

namespace Library.Services.MailService
{
    public interface IMailService
    {
        bool SendMail(MailData mailData);
    }
}
