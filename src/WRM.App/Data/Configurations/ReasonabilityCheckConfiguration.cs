using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WRM.Domain.Entities;

namespace WRM.App.Data.Configurations
{
    public class ReasonabilityCheckConfiguration : IEntityTypeConfiguration<ReasonabilityCheck>
    {
        public void Configure(EntityTypeBuilder<ReasonabilityCheck> builder)
        {
            builder
            .HasIndex(m => new { m.MeasurementId, m.MaxValue, m.MinValue })
            .IsUnique();
        }
    }
}
