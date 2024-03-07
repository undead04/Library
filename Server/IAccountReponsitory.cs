
using Microsoft.AspNetCore.Identity;
using SchoolLibrary.Model;

namespace Library.Server
{
    public interface IAccountReponsitory
    {
        
        public Task<string> SignInAsync(SignInModel model);
    }
}
