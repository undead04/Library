using Microsoft.AspNetCore.Identity;

namespace Library.Services.ClaimsService
{
    public interface IClaimService
    {
        Task CreateClaims(IdentityRole role, string claimsType, string claimsValue);
        Task<IdentityRole> DeleteClaims(IdentityRole role);
    }
}
