using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VacationPortal.DataAccess.Data.EntityConfigurations;
using VacationPortal.Models;

namespace VacationPortal.DataAccess.Data
{
    public class ApplicationDbContext:IdentityDbContext<User, UserRole, int>
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<VacationApplication> VacationApplications { get; set; }
        public DbSet<VacationInfo> VacationInfos { get; set; }
        public DbSet<User> Users2 { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new PositionConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            modelBuilder.Entity<UserRole>().HasData(new UserRole()
            {
                Id = 1,
                Name = "Admin",
                NormalizedName = "ADMIN",
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}