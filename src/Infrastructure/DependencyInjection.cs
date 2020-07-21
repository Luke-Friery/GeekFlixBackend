using Application.Common.Interfaces;
using CleanArchiTemplate.Application.Common.Interfaces;
//using CleanArchiTemplate.Infrastructure.Files;
using CleanArchiTemplate.Infrastructure.Identity;
using CleanArchiTemplate.Infrastructure.Persistence;
using CleanArchiTemplate.Infrastructure.Services;
using Infrastructure.TMDbConnection.Commands;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchiTemplate.Infrastructure
{
  public static class DependencyInjection
  {
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

      //PostgreSQL Database

      services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(
          configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));



      services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

      services.AddDefaultIdentity<ApplicationUser>()
          .AddEntityFrameworkStores<ApplicationDbContext>();

      services.AddIdentityServer()
          .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

      services.AddTransient<IDateTime, DateTimeService>();
      services.AddTransient<IIdentityService, IdentityService>();
      //services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();

      services.AddAuthentication()
          .AddIdentityServerJwt();

      return services;
    }
  }
}
