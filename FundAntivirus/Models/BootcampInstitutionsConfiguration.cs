using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FundAntivirus.Models;

namespace FundAntivirus.Configurations
{
    public class BootcampInstitutionsConfiguration : IEntityTypeConfiguration<BootcampInstitution>
    {
        public void Configure(EntityTypeBuilder<BootcampInstitution> builder)
        {
            builder.HasKey(bi => bi.Id);

            builder.HasOne(bi => bi.Institution)
                .WithMany()
                .HasForeignKey(bi => bi.InstitutionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(bi => bi.Bootcamp)
                .WithMany()
                .HasForeignKey(bi => bi.BootcampId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(bi => bi.Topic)
                .WithMany()
                .HasForeignKey(bi => bi.TopicId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
