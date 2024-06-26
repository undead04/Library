﻿using Library.Data;
using Library.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Library.Repository.AccountReponsitory
{
    public class AccountReponsitory : IAccountReponsitory
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IConfiguration configuration;
        private readonly RoleManager<Role> roleManager;

        public AccountReponsitory(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration, RoleManager<Role> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.roleManager = roleManager;

        }

        public async Task<string> SignInAsync(SignInModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            var result = await userManager.CheckPasswordAsync(user, model.Password);

            if (user == null)
            {
                return string.Empty;
            }

            var role = await roleManager.FindByIdAsync(model.RoleId);
            bool isRole = role.Id == model.RoleId;

            if (!result || !isRole || user == null)
            {
                return string.Empty;
            }


            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,model.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())

            };
            var userRoles = await userManager.GetRolesAsync(user);
            var roleClaims = await roleManager.FindByIdAsync(model.RoleId);
            var usersClaims = await roleManager.GetClaimsAsync(roleClaims);
            foreach (var roles in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
            }
            foreach (var claims in usersClaims)
            {
                authClaims.Add(new Claim(claims.Type, claims.Value));
            }
            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(7),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha256Signature));
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
