using Library.Model;

namespace Library.Server
{
    public interface IMailService
    {
        bool SendMail(MailData mailData);
    }
}
