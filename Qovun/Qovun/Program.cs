

using Application;
using Infrastructure;
using Infrastructure.Repositories;

namespace Qovun
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            IConfiguration configuration = builder.Configuration;
            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(configuration);
            builder.Services.AddGraphQLServer().AddMutationType<QovunerService>();
            //builder.Services.AddGraphQLServer().AddQueryType<ProjectService>();
            var app = builder.Build();
            
            
            app.MapGraphQL();
            app.Run();
        }
    }
}