using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace MusicMixology.Data
{
    // This static class handles seeding default roles and an admin user
    public static class RoleSeeder
    {
        // This method seeds predefined roles and an admin user into the Identity system
        public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
        {
            // Retrieve RoleManager and UserManager services from the dependency injection container
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            // Define default roles for the application
            string[] roleNames = { "Admin", "User" };

            // Loop through each role and create it if it doesn't already exist
            foreach (var role in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Define credentials for the default admin user
            var adminEmail = "admin@musicmixology.com";
            var adminPassword = "Admin@123"; // Note: Use a secure password in production!

            // Check if an admin user with the specified email already exists
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                // If admin user doesn't exist, create a new user with admin credentials
                var newAdmin = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                // Attempt to create the admin user with the given password
                var result = await userManager.CreateAsync(newAdmin, adminPassword);
                if (result.Succeeded)
                {
                    // If user creation succeeded, add the new user to the Admin role
                    await userManager.AddToRoleAsync(newAdmin, "Admin");
                }
            }
            else
            {
                // If the admin user already exists, ensure they're in the Admin role
                if (!(await userManager.IsInRoleAsync(adminUser, "Admin")))
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}
