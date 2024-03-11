using Library.Model;

namespace Library.Server.ReissuePassword
{
    public interface IReissuePassword
    {
        Task ReissuePassword(ReissuePasswordModel model);
        string CreatPassword();
    }
}
