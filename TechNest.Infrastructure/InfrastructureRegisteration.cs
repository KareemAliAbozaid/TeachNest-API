using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TechNest.Domain.Interfaces;
using TechNest.Infrastructure.Data;
using TechNest.Infrastructure.Repositories;

namespace TechNest.Infrastructure
{
    public static class InfrastructureRegisteration
    {
        public static IServiceCollection AddInfrastructureConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            // Register Repositories and UnitOfWork
            services.AddScoped(typeof(IRepositories<>),typeof(Repositories<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Register DbContext and Repositories here
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("TechNest")));

            // Register AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
    }
}
