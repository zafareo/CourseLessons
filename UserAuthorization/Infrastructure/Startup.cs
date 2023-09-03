using Application.Abstractions;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class Startup
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IApplicationDbContext,UserDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DbConnection")));
            
            return services;

        }
    }
}
