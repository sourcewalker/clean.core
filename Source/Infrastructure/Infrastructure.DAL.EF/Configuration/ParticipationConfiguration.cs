using Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Infrastructure.DAL.EF.Configuration
{
    public class ParticipationConfiguration : IEntityTypeConfiguration<Participation>
    {
        public void Configure(EntityTypeBuilder<Participation> builder)
        {
            #region Table Configuration
            builder.ToTable("Participation");
            #endregion

            #region Column Configuration
            builder.HasKey(p => p.Id);

            builder.Property(p => p.CreatedDate)
                    .IsRequired()
                    .HasDefaultValue(DateTimeOffset.UtcNow);

            builder.Property(p => p.Status)
                    .IsRequired()
                    .HasMaxLength(50);

            builder.HasOne(p => p.Site)
                   .WithMany()
                   .HasForeignKey(p => p.SiteId);
            #endregion

            #region Index Configuration
            builder.HasIndex(p => p.Status)
                   .HasName("IX_Participation_Status");
            #endregion
        }
    }
}
