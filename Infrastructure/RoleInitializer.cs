using Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class RoleInitializer
    {
        public static async Task InitializeAsync(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();
            var configuration = services.GetRequiredService<IConfiguration>();

            string email = configuration["AdminEmail"];
            string password = configuration["AdminPassword"];
            string firstName = configuration["AdminFirstName"];
            string lastName = configuration["AdminLastName"];

            if (await roleManager.FindByNameAsync("Admin") == null)
            {
                await roleManager.CreateAsync(new ApplicationRole("Admin"));
            }

            if (await userManager.FindByNameAsync(email) == null)
            {
                var admin = new ApplicationUser
                {
                    Email = email,
                    UserName = email,
                    FirstName = firstName,
                    LastName = lastName
                };

                var result = await userManager.CreateAsync(admin, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRolesAsync(admin, new string[] { "Admin" });
                }
            }
        }
    }
}
