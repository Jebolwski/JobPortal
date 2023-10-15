using JobPortal.Data.Mapping;
using JobPortal.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Data.Context
{
    public class BaseContext : IdentityDbContext
    {
        private readonly string dbUser = "admin";
        private readonly string dbPassword = "password";
        private readonly string dbHost = "localhost";
        private readonly string database = "admin";
        private readonly int dbPort = 5432;
        private readonly string connectionString;

        public DbSet<JobAd> jobAds { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public BaseContext()
        {
            connectionString = $"User ID={dbUser};Password={dbPassword};Host={dbHost};Port={dbPort};Database={database};Pooling=true;";
        }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new JobAdMap());
            modelBuilder.ApplyConfiguration(new JobAdPhotoMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new RoleMap());
            modelBuilder.HasDefaultSchema("JobPortal");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}
