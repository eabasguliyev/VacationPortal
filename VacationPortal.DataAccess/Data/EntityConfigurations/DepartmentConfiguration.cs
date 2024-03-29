﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationPortal.Models;

namespace VacationPortal.DataAccess.Data.EntityConfigurations
{
    public class DepartmentConfiguration: IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(d => d.ShortName).HasMaxLength(128);
            builder.Property(d => d.FullName).IsRequired().HasMaxLength(128);
            builder.Property(d => d.Description).HasMaxLength(255);

            builder.HasMany(d => d.Employees)
                .WithOne(e => e.Department)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasQueryFilter(d => d.ModelStatus != ModelStatus.Deleted);

            builder.HasData(new Department()
            {
                Id = 1,
                ShortName = "HR",
                FullName = "Human Resource",
                CreatedDate = DateTime.Now,
            });

            builder.HasData(new Department()
            {
                Id = 2,
                ShortName = "IT",
                FullName = "Information Technology",
                CreatedDate = DateTime.Now,
            });
        }
    }
}
