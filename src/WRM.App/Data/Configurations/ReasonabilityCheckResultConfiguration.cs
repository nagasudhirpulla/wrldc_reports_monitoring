using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WRM.Domain.Entities;

namespace WRM.App.Data.Configurations
{
    public class ReasonabilityCheckResultConfiguration : IEntityTypeConfiguration<ReasonabilityCheckResult>
    {
        public void Configure(EntityTypeBuilder<ReasonabilityCheckResult> builder)
        {
            builder
            .HasIndex(m => new { m.ReasonabilityCheckId, m.DateOfCheck })
            .IsUnique();
        }
    }
}
