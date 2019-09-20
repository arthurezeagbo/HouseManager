using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using Data;
using Data.Model;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;

namespace Api
{
    public class Program
    {
        
        public static void Main(string[] args)
        {

            Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                    .Enrich.FromLogContext()
                    .WriteTo.File("Logs\\HouseManager.txt", rollingInterval: RollingInterval.Day, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                    .CreateLogger();

            IWebHost host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var service = scope.ServiceProvider;

                IHostingEnvironment _hostingEnvironment = service.GetService<IHostingEnvironment>();
                var contentRoot = _hostingEnvironment.ContentRootPath;

                ApplicationDbContext context = service.GetRequiredService<ApplicationDbContext>();
                UserManager<UserProfileModel> userManager = service.GetRequiredService<UserManager<UserProfileModel>>();
                RoleManager<ApplicationRoleModel> roleManager = service.GetRequiredService<RoleManager<ApplicationRoleModel>>();

                SeedData.SetUpDatabaseAsync(context, userManager, roleManager).GetAwaiter().GetResult();

                //var allUsers = GetAllAsync(context).GetAwaiter().GetResult();
            }

            host.Run();
        }

        public static async System.Threading.Tasks.Task<List<object>> GetAllAsync(ApplicationDbContext context)
        {
            List<object> all = new List<object>();

            using (SqlConnection sqlConnection = new SqlConnection(context.Database.GetDbConnection().ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("spGetAllUser", sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    
                    await sqlConnection.OpenAsync();

                    using (var reader = await sqlCommand.ExecuteReaderAsync())
                    {
                        while(await reader.ReadAsync())
                        {
                            all.Add(MapToValues(reader));
                        }
                        
                    }
                }
            }

            return all;
        }

        public static object MapToValues(SqlDataReader reader)
        {
            var result = new
            {
                Id = reader.GetValue(0).ToString(),
                Email = reader.GetValue(1).ToString()
            };
            
           
            return result;

        }


        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
           
            WebHost.CreateDefaultBuilder(args)
            .UseContentRoot(Directory.GetCurrentDirectory())
            .ConfigureAppConfiguration((hostingContext, config) => {

                var env = hostingContext.HostingEnvironment;

                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                      .AddJsonFile($"appsettings.{env.EnvironmentName}.json",
                          optional: true, reloadOnChange: true);

                config.AddEnvironmentVariables();

            })
            .UseStartup<Startup>()
            .UseSerilog();
    }
}
