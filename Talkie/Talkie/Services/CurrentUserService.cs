using Application.Common.Abstraction;
using System.Security.Claims;

namespace GapKo_p.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IApplicationDbContext _applicationDbContext;

        public CurrentUserService(IHttpContextAccessor httpcontextAccessor, IApplicationDbContext applicationDbContext)
              => (_httpContextAccessor, _applicationDbContext) = (httpcontextAccessor, applicationDbContext);

        public string? Name => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);

        public Guid? Id => _applicationDbContext?.Users?.SingleOrDefault(x => x.Name == this.Name)?.Id;
    }
}
