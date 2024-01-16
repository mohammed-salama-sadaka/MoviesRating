using Microsoft.AspNetCore.Identity;
using MoviesRating.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesRating.Data
{
    public static class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<User> userManager,RoleManager<IdentityRole> roleManager)
        {
            foreach (var role in Enum.GetNames(typeof(Roles)))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
        public static async Task SeedSuperAdminAsync(UserManager<User>userManager,RoleManager<IdentityRole> roleManager)
        {
            var defaultUser = new User
            {
                UserName = "Fizoo",
                Email = "Fizoo@Test.com",
                FirstName = "Ahmed",
                LastName = "AbdelHafeez",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };
            if(userManager.Users.All(i=>i.Id!=defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if(user==null)
                {
                    await userManager.CreateAsync(defaultUser, "Zxc32!");
                    foreach (var role in Enum.GetNames(typeof(Roles)))
                    {
                        await userManager.AddToRoleAsync(defaultUser,role);
                    }
                }
            }
        }
    }
}
