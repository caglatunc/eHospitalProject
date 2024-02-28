using eHospitalServer.Business.Services;
using eHospitalServer.DataAccess.Context;
using eHospitalServer.DataAccess.Services;
using eHospitalServer.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Scrutor;
using System.Reflection;
using System.Text;

namespace eHospitalServer.DataAccess;
public static class DependencyInjection
{
    public static IServiceCollection AddDataAccess(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("PostgreSQL"))
            .UseSnakeCaseNamingConvention();
        });

        services.AddIdentity<User, IdentityRole<Guid>>(cfr =>
        {
            cfr.Password.RequiredLength = 1;
            cfr.Password.RequireNonAlphanumeric = false;
            cfr.Password.RequireUppercase = false;
            cfr.Password.RequireLowercase = false;
            cfr.Password.RequireDigit = false;
            cfr.SignIn.RequireConfirmedEmail = true;
            cfr.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            cfr.Lockout.MaxFailedAccessAttempts = 3;
            cfr.Lockout.AllowedForNewUsers = true;
        })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.AddAuthentication().AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidIssuer = "Cagla Tunc Savas",
                ValidAudience = "School Application",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("my secret key my secret key my secret key 1234...my secret key my secret key my secret key 1234..."))
            };
        });

        services.AddAuthorization();

        // services.AddScoped<IAuthService, AuthService>();
        services.Scan(action =>
        {
            action
                .FromAssemblies(Assembly.GetExecutingAssembly())
                .AddClasses(publicOnly: false)
                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsMatchingInterface()
                .WithScopedLifetime();
        });

        return services;

       
    }
}
