using Microsoft.EntityFrameworkCore;
using FundAntivirus.Models;
namespace FundAntivirus.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }

        public DbSet<Institution> Institutions { get; set; }
    }
}