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
      
        public static async Task SetUpDatabaseAsync(ApplicationDbContext _context, UserManager<UserProfileModel> _userManager, RoleManager<ApplicationRoleModel> _roleManager)
        {
            try
            {
                if (!await _context.Roles.AnyAsync())
                {
                    await _context.Roles.AddAsync(new ApplicationRoleModel(ApplicationRoles.ADMIN));
                    await _context.Roles.AddAsync(new ApplicationRoleModel(ApplicationRoles.SUPER_ADMIN));
                    await _context.Roles.AddAsync(new ApplicationRoleModel(ApplicationRoles.HELPER));
                    await _context.Roles.AddAsync(new ApplicationRoleModel(ApplicationRoles.GUARANTOR));
                    await _context.Roles.AddAsync(new ApplicationRoleModel(ApplicationRoles.EMPLOYER));

                    await _context.SaveChangesAsync();
                }

                if (!await _context.Users.AnyAsync())
                {
                    if (await _roleManager.RoleExistsAsync(ApplicationRoles.SUPER_ADMIN))
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

                        IdentityResult result = await _userManager.CreateAsync(superAdminUser, "Password123");

                        if (result.Succeeded)
                        {
                            Console.WriteLine("Super admin user created");

                            IdentityResult addUserToRole = await _userManager.AddToRoleAsync(superAdminUser, ApplicationRoles.SUPER_ADMIN);

                            if (addUserToRole.Succeeded)
                            {
                                Console.WriteLine("Assigned user super admin role");
                            }
                        }
                    }

                    if (await _roleManager.RoleExistsAsync(ApplicationRoles.ADMIN))
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

                        IdentityResult result = await _userManager.CreateAsync(adminUser, "Password123");

                        if (result.Succeeded)
                        {
                            Console.WriteLine("Admin user created");

                            IdentityResult addUserToRole = await _userManager.AddToRoleAsync(adminUser, ApplicationRoles.ADMIN);

                            if (addUserToRole.Succeeded)
                            {
                                Console.WriteLine("Assigned user admin role");
                            }
                        }
                    }

                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex) { }
            
        }
    }
}
