using Entities;
using Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using Services;

namespace ContactsManager.StartupExtensions
{
    public static class ConfigureServicesExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add services to the container.
            services.AddControllersWithViews();

            //add services
            services.AddScoped<ICountriesService, CountriesService>();
            services.AddScoped<IPersonsService, PersonsService>();

           
            //Enable Identity in this project
            services.AddIdentity<ApplicationUser, ApplicationRole>(options => {
                options.Password.RequiredLength = 5;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireDigit = false;
                options.Password.RequiredUniqueChars = 3;
            })
                             .AddEntityFrameworkStores<PersonsDbContext>()
                    .AddDefaultTokenProviders()
                    .AddUserStore<UserStore<ApplicationUser, ApplicationRole, PersonsDbContext, Guid>>()
                    .AddRoleStore<RoleStore<ApplicationRole, PersonsDbContext, Guid>>();

            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build(); //enforces authoriation policy (user must be authenticated) for all the action methods
            });

            services.ConfigureApplicationCookie(options => {
                options.LoginPath = "/Account/Login";
            });

            //database
            services.AddDbContext<PersonsDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}
