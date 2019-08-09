using Data.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Data
{
    public class HomeManagerContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            
            return new ApplicationDbContext(optionsBuilder.Options);
        }

       
    }

    public class ApplicationDbContext : IdentityDbContext<UserProfileModel,
        ApplicationRoleModel, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("housemanager");

            builder.Entity<HelperModel>(c => 
            {
                c.HasKey(d => d.Id);
                c.HasOne(d => d.Guarantor);
                c.HasOne(d => d.Employer);
                c.Property(d => d.Guarantor).IsRequired(true);
                c.Property(d => d.Employer).IsRequired(false);
                c.Property(d => d.Id).HasColumnType("int").HasColumnName("HelperId").IsRequired(true);
                c.ToTable("Helper");
            });

            builder.Entity<GuarantorModel>(c =>
            {
                c.HasKey(d => d.Id);
                c.HasMany(d => d.Helpers);
                c.HasMany(d => d.Employers);
                c.ToTable("Guarantor");
                c.Property(d => d.Id).HasColumnType("int").HasColumnName("GuarantorId").IsRequired(true);
            });

            builder.Entity<EmployerModel>(c =>
            {
                c.HasKey(d => d.Id);
                c.HasMany(d => d.Helpers);
                c.ToTable("Employer");
                c.Property(d => d.Id).HasColumnType("int").HasColumnName("EmployerId").IsRequired(true);
            });

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {

                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();

                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly("Data"));
          
            }
        }

        public DbSet<HelperModel> Helper { get; set; }
        public DbSet<EmployerModel> Employer { get; set; }
        public DbSet<GuarantorModel> Guarantor { get; set; }
        public DbSet<MessageModel> Message { get; set; }

    }
}
