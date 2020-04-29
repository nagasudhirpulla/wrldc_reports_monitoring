using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WRM.Domain.Entities;

namespace WRM.App.Data.Configurations
{
    public class NonNullCheckConfiguration : IEntityTypeConfiguration<NonNullCheck>
    {
        public void Configure(EntityTypeBuilder<NonNullCheck> builder)
        {
            builder
            .HasIndex(m => m.MeasurementId)
            .IsUnique();
        }
    }
}
