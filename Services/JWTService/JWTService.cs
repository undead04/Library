using DocumentFormat.OpenXml.Spreadsheet;
using Library.Data;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Library.Services.JWTService
{
    public class JWTService : IJWTSevice
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> userManager;

        public JWTService(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }
        public async Task<string> ReadToken()

        {
            var result = string.Empty;
            var UserId = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
                if (result != null)
                {
                    var user = await userManager.FindByEmailAsync(result);
                    return user.Id;
                }

            }
            return null;
            
        }
    }
}
