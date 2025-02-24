using Microsoft.EntityFrameworkCore;
using FundAntivirus.Models;

namespace FundAntivirus.Data
{
    /// <summary>
    /// Contexto de base de datos para Entity Framework Core.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}

        /// <summary>
        /// Conjunto de datos para la entidad User.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Conjunto de datos para la entidad Category.
        /// </summary>
        public DbSet<Category> Categories {get;set;}

        /// <summary>
        /// Conjunto de datos para la entidad Opportunity.
        /// </summary>
        public DbSet<Opportunity> Opportunities{get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .Property(u => u.Id)
                .ValueGeneratedNever(); // Evita que EF trate de manejarlo como IDENTITY

            // Configurar AuditInfo como propiedad embebida en Opportunity
            modelBuilder.Entity<Opportunity>()
                .OwnsOne(o => o.AuditInfo);
        }
    }
}