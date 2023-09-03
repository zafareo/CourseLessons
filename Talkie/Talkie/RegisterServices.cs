using Application.Common.Abstraction;
using GapKo_p.Services;

namespace GapKo_p
{
    public static class RegisterServices
    {
        public static IServiceCollection AddWebUIService(this IServiceCollection services)
        {
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddHttpContextAccessor();
            return services;
        }
    }
}
