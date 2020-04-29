using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WRM.Domain.Entities;

namespace WRM.App.Data.Configurations
{
    public class ZscoreCheckResultConfiguration : IEntityTypeConfiguration<ZscoreCheckResult>
    {
        public void Configure(EntityTypeBuilder<ZscoreCheckResult> builder)
        {
            builder
            .HasIndex(m => new { m.ZscoreCheckId, m.DateOfCheck })
            .IsUnique();
        }
    }
}
