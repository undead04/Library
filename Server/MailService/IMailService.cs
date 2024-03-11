using Library.Model;

namespace Library.Server.MailService
{
    public interface IMailService
    {
        bool SendMail(MailData mailData);
    }
}
