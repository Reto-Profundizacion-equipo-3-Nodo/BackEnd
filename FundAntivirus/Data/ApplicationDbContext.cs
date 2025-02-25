using Microsoft.EntityFrameworkCore;
using FundAntivirus.Models;
namespace FundAntivirus.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<User> User { get; set; }
        public DbSet<BootcampInstitution> BootcampInstitutions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.ApplyConfiguration(new FundAntivirus.Configurations.BootcampInstitutionsConfiguration());
        }
    }
}