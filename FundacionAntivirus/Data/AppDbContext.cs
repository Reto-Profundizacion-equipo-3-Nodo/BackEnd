using System;
using System.Collections.Generic;
using FundacionAntivirus.Models;
using Microsoft.EntityFrameworkCore;

namespace FundacionAntivirus.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext() { }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public virtual DbSet<BootcampTopics> BootcampTopics { get; set; }
    public virtual DbSet<Bootcamps> Bootcamps { get; set; }
    public virtual DbSet<Categories> Categories { get; set; }
    public virtual DbSet<Institution> Institutions { get; set; }
    public virtual DbSet<Opportunities> Opportunities { get; set; }
    public virtual DbSet<Topics> Topics { get; set; }
    public virtual DbSet<UserOpportunities> UserOpportunities { get; set; }
    public virtual DbSet<Users> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Database=Antivirus;Username=postgres;Password=123987");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureBootcampTopics(modelBuilder);
        ConfigureBootcamps(modelBuilder);
        ConfigureCategories(modelBuilder);
        ConfigureInstitutions(modelBuilder);
        ConfigureOpportunities(modelBuilder);
        ConfigureTopics(modelBuilder);
        ConfigureUserOpportunities(modelBuilder);
        ConfigureUsers(modelBuilder);
        
        OnModelCreatingPartial(modelBuilder);
    }

    private void ConfigureBootcampTopics(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BootcampTopics>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("bootcamp_topics_pkey");

            entity.HasOne(d => d.Bootcamp)
                .WithMany(p => p.BootcampTopics)
                .HasConstraintName("bootcamp_topics_bootcamp_id_fkey");

            entity.HasOne(d => d.Topic)
                .WithMany(p => p.BootcampTopics)
                .HasConstraintName("bootcamp_topics_topic_id_fkey");
        });
    }

    private void ConfigureBootcamps(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bootcamps>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("bootcamps_pkey");

            entity.HasOne(d => d.Institution)
                .WithMany(p => p.Bootcamps)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("bootcamps_institution_id_fkey");
        });
    }

    private void ConfigureCategories(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categories>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("categories_pkey");
        });
    }

    private void ConfigureInstitutions(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Institution>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("institutions_pkey");
        });
    }

    private void ConfigureOpportunities(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Opportunities>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("opportunities_pkey");

            entity.HasOne(d => d.Category)
                .WithMany(p => p.Opportunities)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("opportunities_category_id_fkey");

            entity.HasOne(d => d.Institution)
                .WithMany(p => p.Opportunities)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("opportunities_institution_id_fkey");
        });
    }

    private void ConfigureTopics(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Topics>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("topics_pkey");
        });
    }

    private void ConfigureUserOpportunities(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserOpportunities>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_opportunities_pkey");

            entity.HasOne(d => d.Opportunity)
                .WithMany(p => p.UserOpportunities)
                .HasConstraintName("user_opportunities_opportunity_id_fkey");

            entity.HasOne(d => d.User)
                .WithMany(p => p.UserOpportunities)
                .HasConstraintName("user_opportunities_user_id_fkey");
        });
    }

    private void ConfigureUsers(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Users>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");
        });
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
