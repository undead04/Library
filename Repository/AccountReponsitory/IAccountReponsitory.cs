using Library.Model;
using Microsoft.AspNetCore.Identity;


namespace Library.Repository.AccountReponsitory
{
    public interface IAccountReponsitory
    {

        public Task<string> SignInAsync(SignInModel model);
    }
}
