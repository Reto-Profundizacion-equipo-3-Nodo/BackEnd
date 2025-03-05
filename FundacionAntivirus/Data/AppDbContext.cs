using FundacionAntivirus.Models;
using Microsoft.EntityFrameworkCore;

namespace FundacionAntivirus.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<BootcampTopic> BootcampTopics { get; set; } = null!;
    public DbSet<Bootcamp> Bootcamps { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Institution> Institutions { get; set; } = null!;
    public DbSet<Opportunity> Opportunities { get; set; } = null!;
    public DbSet<Topic> Topics { get; set; } = null!;
    public DbSet<UserOpportunity> UserOpportunities { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("Name=ConnectionStrings:DefaultConnection");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BootcampTopic>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("bootcamp_topics_pkey");

            entity.HasOne(d => d.Bootcamp)
                .WithMany(p => p.BootcampTopics)
                .HasForeignKey(d => d.BootcampId)
                .HasConstraintName("bootcamp_topics_bootcamp_id_fkey");

            entity.HasOne(d => d.Topic)
                .WithMany(p => p.BootcampTopics)
                .HasForeignKey(d => d.TopicId)
                .HasConstraintName("bootcamp_topics_topic_id_fkey");
        });

        modelBuilder.Entity<Bootcamp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("bootcamps_pkey");

            entity.HasOne(d => d.Institution)
                .WithMany(p => p.Bootcamps)
                .HasForeignKey(d => d.InstitutionId)
                .OnDelete(DeleteBehavior.SetNull) // Evitar cascada riesgosa
                .HasConstraintName("bootcamps_institution_id_fkey");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("categories_pkey");
        });

        modelBuilder.Entity<Institution>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("institutions_pkey");
        });

        modelBuilder.Entity<Opportunity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("opportunities_pkey");

            entity.HasOne(d => d.Category)
                .WithMany(p => p.Opportunities)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("opportunities_category_id_fkey");

            entity.HasOne(d => d.Institution)
                .WithMany(p => p.Opportunities)
                .HasForeignKey(d => d.InstitutionId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("opportunities_institution_id_fkey");

            entity.HasIndex(o => o.Name) // Mejora en búsquedas
                .HasDatabaseName("idx_opportunity_name");
        });

        modelBuilder.Entity<Topic>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("topics_pkey");
        });

        modelBuilder.Entity<UserOpportunity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_opportunities_pkey");

            entity.HasOne(d => d.Opportunity)
                .WithMany(p => p.UserOpportunities)
                .HasForeignKey(d => d.OpportunityId)
                .OnDelete(DeleteBehavior.Restrict) // Evitar eliminación accidental
                .HasConstraintName("user_opportunities_opportunity_id_fkey");

            entity.HasOne(d => d.User)
                .WithMany(p => p.UserOpportunities)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("user_opportunities_user_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}