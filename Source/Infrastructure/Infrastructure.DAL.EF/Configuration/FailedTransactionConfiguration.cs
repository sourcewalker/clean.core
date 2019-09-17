using Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Infrastructure.DAL.EF.Configuration
{
    public class FailedTransactionConfiguration : IEntityTypeConfiguration<FailedTransaction>
    {
        public void Configure(EntityTypeBuilder<FailedTransaction> builder)
        {
            #region Table Configuration
            builder.ToTable("FailedTransaction");
            #endregion

            #region Column Configuration
            builder.HasKey(ft => ft.Id);

            builder.Property(ft => ft.CreatedDate)
                    .IsRequired()
                    .HasDefaultValue(DateTimeOffset.UtcNow);

            builder.HasOne(ft => ft.Participation)
                   .WithMany()
                   .HasForeignKey(ft => ft.ParticipationId);

            builder.Property(ft => ft.NewsletterOptin)
                    .IsRequired();
            #endregion
        }
    }
}
