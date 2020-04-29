using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WRM.Domain.Entities;

namespace WRM.App.Data.Configurations
{
    public class NonNullCheckResultConfiguration : IEntityTypeConfiguration<NonNullCheckResult>
    {
        public void Configure(EntityTypeBuilder<NonNullCheckResult> builder)
        {
            builder
            .HasIndex(m => new { m.NonNullCheckId, m.DateOfCheck })
            .IsUnique();
        }
    }
}
