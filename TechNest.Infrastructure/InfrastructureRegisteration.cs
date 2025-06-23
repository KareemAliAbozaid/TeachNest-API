using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TechNest.Domain.Interfaces;
using TechNest.Infrastructure.Repositories;

namespace TechNest.Infrastructure
{
    public static class InfrastructureRegisteration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IRepositories<>),typeof(Repositories<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Register DbContext and Repositories here
            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            //services.AddScoped(typeof(IRepositories<>), typeof(Repositories<>));

            return services;
        }
    }
}
