using Microsoft.AspNetCore.Identity;
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

            var ph = new PasswordHasher<User>();

            var employee1 = new Employee()
            {
                Id = 1,
                FirstName = "Elgun",
                LastName = "Abasquliyev",
                Email = "elgun@gmail.com",
                UserName = "elgun@gmail.com",
                NormalizedUserName = "ELGUN@GMAIL.COM",
                NormalizedEmail = "ELGUN@GMAIL.COM",
                PositionId = 2,
                DepartmentId = 2,
                CreatedDate = DateTime.Now,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            employee1.PasswordHash = ph.HashPassword(employee1, "Elgun1234!");

            var employee2 = new Employee()
            {
                Id = 2,
                FirstName = "Senan",
                LastName = "Memmedov",
                Email = "senan@gmail.com",
                UserName = "senan@gmail.com",
                NormalizedUserName = "SENAN@GMAIL.COM",
                NormalizedEmail = "SENAN@GMAIL.COM",
                PositionId = 2,
                DepartmentId = 1,
                CreatedDate = DateTime.Now,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            employee2.PasswordHash = ph.HashPassword(employee2, "Senan1234!");

            var employee3 = new Employee()
            {
                Id = 3,
                FirstName = "Arif",
                LastName = "Baghirli",
                Email = "arif@gmail.com",
                UserName = "arif@gmail.com",
                NormalizedUserName = "ARIF@GMAIL.COM",
                NormalizedEmail = "ARIF@GMAIL.COM",
                PositionId = 1,
                DepartmentId = 2,
                CreatedDate = DateTime.Now,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            employee3.PasswordHash = ph.HashPassword(employee3, "Arif1234!");

            builder.HasData(employee1);
            builder.HasData(employee2);
            builder.HasData(employee3);
        }
    }
}
