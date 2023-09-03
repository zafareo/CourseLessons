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
    public class UpdatePermissionCommand : IRequest<bool>
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
    }
    public class UpdatePermissionCommandHandler : IRequestHandler<UpdatePermissionCommand, bool>
    {
        private IApplicationDbContext _context;
        private ICurrentUserService _currentUserService;

        public UpdatePermissionCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
               => (_context, _currentUserService) = (context, currentUserService);


        public async Task<bool> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Permissions.FindAsync(new object[] { request.Id }, cancellationToken);
            if (entity != null)
            {
                entity.Name = request.Name;
                entity.LastUpdatedDate = DateTime.UtcNow;
                entity.LastUpdatedBy = _currentUserService.Name;
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
           return false;
        }
    }
}
