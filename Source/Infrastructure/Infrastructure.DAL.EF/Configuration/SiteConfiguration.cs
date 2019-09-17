using Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Infrastructure.DAL.EF.Configuration
{
    public class SiteConfiguration : IEntityTypeConfiguration<Site>
    {
        public void Configure(EntityTypeBuilder<Site> builder)
        {
            #region Table Configuration
            builder.ToTable("Site");
            #endregion

            #region Column Configuration
            builder.HasKey(s => s.Id);

            builder.Property(s => s.CreatedDate)
                    .IsRequired()
                    .HasDefaultValue(DateTimeOffset.UtcNow);
            builder.Property(s => s.Culture)
                    .IsRequired()
                    .HasMaxLength(10);

            builder.Property(s => s.Domain)
                    .HasMaxLength(50);
            #endregion

            #region Index Configuration
            builder.HasIndex(s => s.Culture)
                   .HasName("IX_Site_Culture");

            builder.HasIndex(s => s.Domain)
                   .HasName("IX_Site_Domain");
            #endregion
        }
    }
}
