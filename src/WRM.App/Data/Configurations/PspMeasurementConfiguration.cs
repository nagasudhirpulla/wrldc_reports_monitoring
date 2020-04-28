using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WRM.Domain.Entities;

namespace WRM.App.Data.Configurations
{
    public class PspMeasurementConfiguration : IEntityTypeConfiguration<PspMeasurement>
    {
        public void Configure(EntityTypeBuilder<PspMeasurement> builder)
        {
            builder
            .HasIndex(m => m.Label)
            .IsUnique();

            builder
                .Property(m => m.QueryString)
                .IsRequired();
        }
    }
}
