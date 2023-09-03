using Application.Abstractions;
using Application.Interfaces;
using Infrastructure.DataAccess;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure;

public static class RegisterServices
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
        options.UseNpgsql(configuration.GetConnectionString("DbConnection")));
        services.AddScoped<IProjectRepository, ProjectService>();
        services.AddScoped<IQovunerRepository, QovunerService>();
        return services;

    }
}
