using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VacationPortal.DataAccess.Data;
using VacationPortal.DataAccess.Repositories;
using VacationPortal.DataAccess.Repositories.Abstracts;
using VacationPortal.Models;
using VacationPortal.Web.Areas.Admin.Models.EmployeeVMs;
using VacationPortal.Web.AuthService.Handlers;
using VacationPortal.Web.AuthService.Requirements;
using VacationPortal.Web.Validations;

namespace VacationPortal.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews()
                    .AddFluentValidation();

            services.AddTransient<IValidator<Department>, DepartmentValidation>();
            services.AddTransient<IValidator<Position>, PositionValidation>();
            services.AddTransient<IValidator<EmployeeVM>, EmployeeValidation>();
            services.AddTransient<IValidator<VacationApplication>, VacationApplicationValidation>();

            services.AddDbContext<ApplicationDbContext>(options =>
                                options.UseSqlServer(Configuration.GetConnectionString("Local")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(typeof(Startup));
            services.AddIdentity<User, UserRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.AddAuthentication();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("HrDepartment", options =>
                {
                    options.AddRequirements(new DepartmentRequirement("HR"));
                });
            });
            services.AddScoped<IAuthorizationHandler, DepartmentHandler>();
            services.ConfigureApplicationCookie(configure =>
            {
                configure.LoginPath = "/Identity/Account/Login";
                configure.LogoutPath = "/Identity/Account/Logout";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area=Identity}/{controller=Account}/{action=Index}/{id?}"
                  );
            });
        }
    }
}
