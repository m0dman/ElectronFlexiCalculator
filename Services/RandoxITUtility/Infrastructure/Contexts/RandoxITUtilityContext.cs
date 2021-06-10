using RandoxITUtility.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using System;
using RandoxITUtility.Infrastructure.Data.Configurations;
using RandoxITUtility.Infrastructure.Data.Contexts;
using RandoxITUtility.Domain;

namespace RandoxITUtility.Infrastructure.Data.Contexts
{
    public partial class RandoxITUtilityContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Test> Tests { get; set; }
        public DbSet<TimeData> WeeklyTimes { get; set; }

        public RandoxITUtilityContext()
        {
        }

        public RandoxITUtilityContext(DbContextOptions<RandoxITUtilityContext> options) : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TestConfig());

            modelBuilder.ApplyConfiguration(new DaysOfTheWeekConfig());
        }

        /// <summary>
        /// Supporting default and global controls for Audit events (dates and edits)
        /// TODO: user info needs to be added!
        /// </summary>
        /// <returns></returns>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.UtcNow;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
