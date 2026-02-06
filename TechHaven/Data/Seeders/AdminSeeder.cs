using Microsoft.AspNetCore.Identity;
using TechHaven.Data.Models;


namespace TechHaven.Data.Seeders;

public static class AdminSeeder
{
    public static async Task SeedAdminRoleAsync(RoleManager<IdentityRole> roleManager)
    {
        string adminRole = "Admin";

        if (!await roleManager.RoleExistsAsync(adminRole))
        {
            await roleManager.CreateAsync(new IdentityRole(adminRole));
        }
    }

    public static async Task EnsureAdminUserAsync(UserManager<ApplicationUser> userManager)
    {
        string adminEmail = "admin@techhaven.com";
        string adminPassword = "Admin123!";

        var user = await userManager.FindByEmailAsync(adminEmail);

        if (user == null)
        {
            user = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };

            await userManager.CreateAsync(user, adminPassword);
        }

        if (!await userManager.IsInRoleAsync(user, "Admin"))
        {
            await userManager.AddToRoleAsync(user, "Admin");
        }
    }
}
