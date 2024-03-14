using Library.Model;

namespace Library.Services.ReissuePassword
{
    public interface IReissuePassword
    {
        Task ReissuePassword(ReissuePasswordModel model);
        string CreatPassword();
    }
}
