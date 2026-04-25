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

            // 1. Ensure the roles exist in the new SQLite database
            string[] roleNames = { "Admin", "Viewer" };

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // 2. PROMOTE YOUR PERSONAL ACCOUNT TO ADMIN
            // This checks if you have already registered through the website
            var personalAccount = await userManager.FindByEmailAsync("sidra.chatta95@gmail.com");
            if (personalAccount != null)
            {
                var isInRole = await userManager.IsInRoleAsync(personalAccount, "Admin");
                if (!isInRole)
                {
                    await userManager.AddToRoleAsync(personalAccount, "Admin");
                }
            }

            // 3. OPTIONAL: Create the default fallback admin
            var defaultAdminEmail = "admin@intern.com";
            var defaultAdmin = await userManager.FindByEmailAsync(defaultAdminEmail);

            if (defaultAdmin == null)
            {
                var user = new IdentityUser
                {
                    UserName = defaultAdminEmail,
                    Email = defaultAdminEmail,
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