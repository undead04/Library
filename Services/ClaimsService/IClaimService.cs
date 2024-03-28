using Library.Data;
using Microsoft.AspNetCore.Identity;

namespace Library.Services.ClaimsService
{
    public interface IClaimService
    {
        Task CreateClaims(Role role, string claimsType, string claimsValue);
        Task<IdentityRole> DeleteClaims(Role role);
    }
}
