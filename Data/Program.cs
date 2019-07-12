using Data.Model;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Settings.Configs;
using Settings.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    class Program
    {
        public static void Main(string[] args)
        {
            IWebHost host = CreateWebHostBuilder(args).Build();

            //using (var scope = host.Services.CreateScope())
            //{
            //    var service = scope.ServiceProvider;

            //    var context = service.GetRequiredService<ApplicationDbContext>();
            //    var userManager = service.GetRequiredService<UserManager<UserProfileModel>>();
            //    var roleManager = service.GetRequiredService<RoleManager<ApplicationRoleModel>>();

            //    SeedData.SetUpDatabaseAsync(context, userManager, roleManager).GetAwaiter().GetResult();
            //}

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
