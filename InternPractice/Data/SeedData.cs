using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace InternPractice.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            string[] roleNames = { "Admin", "Viewer" }; // Task A3.2: Admin and Viewer roles 

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                     
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Optional: Create a default Admin user so you can test immediately
            var adminUser = await userManager.FindByEmailAsync("admin@intern.com");
            if (adminUser == null)
            {
                var user = new IdentityUser
                {
                    UserName = "admin@intern.com",
                    Email = "admin@intern.com",
                    EmailConfirmed = true
                };

                var createPowerUser = await userManager.CreateAsync(user, "Admin@123");
                if (createPowerUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }
    }
}