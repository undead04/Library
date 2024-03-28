using Library.Data;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Library.Services.ClaimsService
{
    public class ClaimService:IClaimService
    {
        private readonly RoleManager<Role> roleManager;

        public ClaimService(RoleManager<Role> roleManager)
        {
            this.roleManager = roleManager;
        }
        public async Task CreateClaims(Role role, string claimsType, string claimsValue)
        {
            var claims = new Claim(claimsType, claimsValue);
            await roleManager.AddClaimAsync(role, claims);
        }

        public async Task<IdentityRole> DeleteClaims(Role role)
        {
            var existingClaims = await roleManager.GetClaimsAsync(role);
            foreach (var claim in existingClaims)
            {
                await roleManager.RemoveClaimAsync(role, claim);
            }
            var result = await roleManager.UpdateAsync(role);
            return role;
        }
    }
}
