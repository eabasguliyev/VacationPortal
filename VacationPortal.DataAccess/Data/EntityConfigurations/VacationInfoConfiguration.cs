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
    public class VacationInfoConfiguration: IEntityTypeConfiguration<VacationInfo>
    {
        public void Configure(EntityTypeBuilder<VacationInfo> builder)
        {
            builder.HasQueryFilter(d => d.ModelStatus != ModelStatus.Deleted);
        }
    }
}
