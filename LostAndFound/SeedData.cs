using LostAndFound.DbContexts;
using LostAndFound.Enums;
using LostAndFound.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace LostAndFound
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            string[] roleNames = { "Admin", "Moderator", "User" };
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole<int>(roleName));
                }
            }

            var adminUser = await userManager.FindByEmailAsync("admin@lostandfound.com");
            if (adminUser == null)
            {
                var admin = new User
                {
                    UserName = "admin@lostandfound.com",
                    Email = "admin@lostandfound.com",
                    FullName = "مدير النظام",
                    IsVerified = true,
                    CreatedAt = DateTime.Now,
                    EmailConfirmed=true,
                    Role = Role.ADMIN
                };

                var result = await userManager.CreateAsync(admin, "Admin@123456");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}