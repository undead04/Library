using Library.Model;

namespace Library.Server
{
    public interface IReissuePassword
    {
        Task ReissuePassword(ReissuePasswordModel model);
    }
}
