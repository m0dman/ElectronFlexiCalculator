using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RandoxITUtility.Domain.Entities;

namespace RandoxITUtility.Infrastructure.Data.Configurations
{
    internal class DaysOfTheWeekConfig : IEntityTypeConfiguration<TimeData>
    {
        public void Configure(EntityTypeBuilder<TimeData> entity)
        {
            entity.HasKey(a => a.Id);
            entity.Property(p => p.Id).HasDefaultValueSql("NEWID()");
            entity.Property(p => p.WeekCommencing).IsRequired();
            entity.Property(p => p.StartTime).IsRequired();
            entity.Property(p => p.EndTime).IsRequired();
            entity.Property(p => p.LunchLength).IsRequired();
        }
    }
}