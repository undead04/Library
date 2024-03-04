using Microsoft.AspNetCore.Identity;

namespace SchoolLibrary.Model
{
    public class SeedRole
    {
        // Create Role
        public static async Task CreateRoleAsync(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                
               AppRole array=new AppRole();
                string[] arrayRole = array.ArrayRole();
                foreach(string role in arrayRole)
                {
                    if (!await roleManager!.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }

            }
        }
    }
}
