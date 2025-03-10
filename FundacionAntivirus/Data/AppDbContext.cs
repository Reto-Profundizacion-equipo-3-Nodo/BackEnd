using FundacionAntivirus.Models;
using Microsoft.EntityFrameworkCore;

namespace FundacionAntivirus.Data;

/// <summary>
/// Contexto de base de datos de la aplicación.
/// </summary>
public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<BootcampTopic> BootcampTopics { get; set; } = null!;
    public DbSet<Bootcamp> Bootcamps { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Institution> Institutions { get; set; } = null!;
    public DbSet<Opportunity> Opportunities { get; set; } = null!;
    public DbSet<OpportunityInstitution> OpportunityInstitutions { get; set; } = null!;
    public DbSet<Topic> Topics { get; set; } = null!;
    public DbSet<UserOpportunity> UserOpportunities { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
     public DbSet<Donation> Donations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            if (!string.IsNullOrEmpty(connectionString))
            {
                optionsBuilder.UseNpgsql(connectionString);
            }
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
