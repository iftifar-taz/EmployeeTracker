using EmployeeTracker.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeTracker.Infrastructure.Persistence
{
    public static class DbSeeder
    {
        public static async Task SeedUsersAndRolesAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Seed roles
            var roles = new[] { "Admin", "Moderator", "User" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Seed admin user
            const string adminEmail = "admin@example.com";
            const string adminPassword = "Pa$$w0rd";

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var adminUser = new User
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // Seed moderator user
            const string moderatorEmail = "moderator@example.com";
            const string moderatorPassword = "Pa$$w0rd";

            if (await userManager.FindByEmailAsync(moderatorEmail) == null)
            {
                var moderatorUser = new User
                {
                    UserName = moderatorEmail,
                    Email = moderatorEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(moderatorUser, moderatorPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(moderatorUser, "Moderator");
                }
            }

            // Seed user user
            const string userEmail = "user@example.com";
            const string userPassword = "Pa$$w0rd";

            if (await userManager.FindByEmailAsync(userEmail) == null)
            {
                var userUser = new User
                {
                    UserName = userEmail,
                    Email = userEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(userUser, userPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(userUser, "User");
                }
            }
        }
    }
}
