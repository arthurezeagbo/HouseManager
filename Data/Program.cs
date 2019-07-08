using Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Settings.Configs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    var services = new ServiceCollection();

        //    var connectionString = DbConnection.CONNECTION_STRING;

        //    services.AddDbContext<ApplicationDbContext>(options =>
        //       options.UseSqlServer(connectionString));

        //    services.AddIdentity<UserProfileModel, IdentityRole>()
        //        .AddEntityFrameworkStores<ApplicationDbContext>()
        //        .AddDefaultTokenProviders();

        //    using (var serviceProvider = services.BuildServiceProvider())
        //    {
        //        using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
        //        {
        //            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
        //            context.Database.Migrate();


        //            var result = SeedData.SetUpDatabaseAsync(serviceProvider);
        //        }
        //    }
        //}
    }
}
