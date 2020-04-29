using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WRM.Domain.Entities;

namespace WRM.App.Data.Configurations
{
    public class ZscoreCheckConfiguration : IEntityTypeConfiguration<ZscoreCheck>
    {
        public void Configure(EntityTypeBuilder<ZscoreCheck> builder)
        {
            builder
            .HasIndex(m => new { m.MeasurementId, m.Threshold, m.NumDays, m.Influence })
            .IsUnique();
        }
    }
}
