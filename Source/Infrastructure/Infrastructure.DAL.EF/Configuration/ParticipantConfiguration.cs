using Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Infrastructure.DAL.EF.Configuration
{
    public class ParticipantConfiguration : IEntityTypeConfiguration<Participant>
    {
        public void Configure(EntityTypeBuilder<Participant> builder)
        {
            #region Table Configuration
            builder.ToTable("Participant");
            #endregion

            #region Column Configuration
            builder.HasKey(p => p.Id);

            builder.Property(p => p.CreatedDate)
                    .IsRequired()
                    .HasDefaultValue(DateTimeOffset.UtcNow);

            builder.Property(p => p.EmailHash)
                    .IsRequired()
                    .HasMaxLength(450);

            builder.HasOne(p => p.Participation)
                   .WithMany()
                   .HasForeignKey(p => p.ParticipationId);
            #endregion

            #region Index Configuration
            builder.HasIndex(p => p.EmailHash)
                   .HasName("IX_Participant_EmailHash");
            #endregion
        }
    }
}
