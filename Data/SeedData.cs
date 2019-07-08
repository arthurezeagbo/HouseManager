using Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Settings.Constants;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class SeedData
    {
        public static async Task SetUpDatabaseAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<UserProfileModel>>();
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            if (!await context.Roles.AnyAsync())
            {
                await context.Roles.AddAsync(new ApplicationRoleModel(ApplicationRoles.ADMIN));
                await context.Roles.AddAsync(new ApplicationRoleModel(ApplicationRoles.SUPER_ADMIN));
                await context.Roles.AddAsync(new ApplicationRoleModel(ApplicationRoles.HELPER));
                await context.Roles.AddAsync(new ApplicationRoleModel(ApplicationRoles.GUARANTOR));
                await context.Roles.AddAsync(new ApplicationRoleModel(ApplicationRoles.EMPLOYER));

                await context.SaveChangesAsync();
            }

            if(!await context.Users.AnyAsync())
            {
                if (!await roleManager.RoleExistsAsync(ApplicationRoles.SUPER_ADMIN))
                {
                    UserProfileModel superAdminUser = new UserProfileModel
                    {
                        Surname = "Ezeagbo",
                        FirstName = "Arthur",
                        LastName = "Afamuefuna",
                        Email = "arthurezeagbo@gmail.com",
                        UserName = "arthurezeagbo@gmail.com",
                        IsActive = true,
                    };

                    IdentityResult result = await userManager.CreateAsync(superAdminUser, "password");

                    if (result.Succeeded)
                    {
                        Console.WriteLine("Super admin user created");

                        IdentityResult addUserToRole = await userManager.AddToRoleAsync(superAdminUser, ApplicationRoles.SUPER_ADMIN);

                        if (addUserToRole.Succeeded)
                        {
                            Console.WriteLine("Assigned user super admin role");
                        }
                    }
                }

                if (!await roleManager.RoleExistsAsync(ApplicationRoles.ADMIN))
                {
                    UserProfileModel adminUser = new UserProfileModel
                    {
                        Surname = "Ezeagbo",
                        FirstName = "Arthur",
                        LastName = "Afamuefuna",
                        Email = "arthur.afamuefuna.ext@lafargeholcim.com",
                        UserName = "arthur.afamuefuna.ext@lafargeholcim.com",
                        IsActive = true,
                    };

                    IdentityResult result = await userManager.CreateAsync(adminUser, "password");

                    if (result.Succeeded)
                    {
                        Console.WriteLine("Admin user created");

                        IdentityResult addUserToRole = await userManager.AddToRoleAsync(adminUser, ApplicationRoles.ADMIN);

                        if (addUserToRole.Succeeded)
                        {
                            Console.WriteLine("Assigned user admin role");
                        }
                    }
                }

                await context.SaveChangesAsync();
            }
        }
    }
}
