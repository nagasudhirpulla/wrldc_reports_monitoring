using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using WRM.Domain.Entities;
using System;
using System.Reflection;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace WRM.App.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<PspMeasurement> PspMeasurements { get; set; }
        public DbSet<NonNullCheck> NonNullChecks { get; set; }
        public DbSet<NonNullCheckResult> NonNullCheckResults { get; set; }
        public DbSet<ReasonabilityCheck> ReasonabilityChecks { get; set; }
        public DbSet<ReasonabilityCheckResult> ReasonabilityCheckResults { get; set; }
        public DbSet<ZscoreCheck> ZscoreChecks { get; set; }
        public DbSet<ZscoreCheckResult> ZscoreCheckResults { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
