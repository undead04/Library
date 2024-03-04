
using Microsoft.AspNetCore.Identity;
using SchoolLibrary.Model;

namespace Library.Server
{
    public interface IAccountReponsitory
    {
        public Task<IdentityResult> SignUpAsync(SignInModel model);
        public Task<string> SignInAsync(SignInModel model);
    }
}
