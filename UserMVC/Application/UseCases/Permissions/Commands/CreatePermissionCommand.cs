using Application.Common.Abstraction;
using Domain.IdentityEntities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MediatrEntities.Permissions.Commands
{
    public class CreatePermissionCommand : IRequest<Guid>
    {
        public string Name { get; set; }
    }
    public class CreatePermissionCommandHandler : IRequestHandler<CreatePermissionCommand, Guid>
    {
        private IApplicationDbContext _context;
        private ICurrentUserService _currentUserService;

        public CreatePermissionCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
               => (_context, _currentUserService) = (context, currentUserService);



        public async Task<Guid> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
        {
            var entity = new Permission
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = _currentUserService.Name
            };
            await _context.Permissions.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;

        }
    }
}
