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
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasQueryFilter(u => u.ModelStatus != ModelStatus.Deleted);

            builder.HasData(new UserRole()
            {
                Id = 1,
                Name = "Admin",
                NormalizedName = "ADMIN",
            });
        }
    }
}
