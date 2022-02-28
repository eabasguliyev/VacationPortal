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
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable(nameof(Employee) + "s");
            builder.Property(e => e.FirstName).IsRequired().HasMaxLength(128);
            builder.Property(e => e.LastName).IsRequired().HasMaxLength(128);

            builder.HasMany(e => e.VacationApplications).WithOne(v => v.Employee).HasForeignKey(v => v.EmployeeId);
        }
    }
}
