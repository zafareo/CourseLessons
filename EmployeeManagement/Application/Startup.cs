using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class Startup
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IJWTservice, JWTService>();
            services.AddScoped<IPermissionRepository,PermissionRepository>();
            services.AddScoped<IProductRepository,ProductRepository>();
            services.AddScoped<IRolePermissionRepository,RolePermissionRepository>();
            services.AddScoped<IRoleRepository,RoleRepository>();
            services.AddScoped<IUserRepository,UserRepository>();
            services.AddScoped<IUserRoleRepository,UserRoleRepository>();
            return services;
        }
    }
}
