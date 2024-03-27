using Library.Data;

namespace Library.Services.JWTService
{
    public interface IJWTSevice
    {
        Task<ApplicationUser> ReadToken();
    }
}
