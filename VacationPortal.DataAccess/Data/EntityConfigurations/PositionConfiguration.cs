using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationPortal.Models;

namespace VacationPortal.DataAccess.Data.EntityConfigurations
{
    public class PositionConfiguration : IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> builder)
        {
            builder.HasMany(p => p.Employees)
                .WithOne(e => e.Position)
                .HasForeignKey(e => e.PositionId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(p => p.VacationInfos)
                .WithOne(v => v.Position)
                .HasForeignKey(v => v.PositionId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasQueryFilter(p => p.ModelStatus != ModelStatus.Deleted);
        }
    }
}
