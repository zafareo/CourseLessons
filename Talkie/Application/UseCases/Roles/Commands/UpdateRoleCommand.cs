using Application.Common.Abstraction;
using Domain.IdentityEntities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MediatrEntities.Roles.Commands
{
    public class UpdateRoleCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Name { get; init; }
        public List<Guid>? PermissionIds { get; init; }
    }
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public UpdateRoleCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
               => (_context, _currentUserService) = (context, currentUserService);

        public async Task<bool> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {

            var entity = await _context.Roles
                .FindAsync(new object[] { request.Id }, cancellationToken);
            if (entity != null)
            { 
                entity.Name = request.Name;
                entity.LastUpdatedDate = DateTime.UtcNow;
                entity.LastUpdatedBy = _currentUserService.Name;
                if (request.PermissionIds is not null)
                {
                    List<Permission> foundPermissions = new();
                    foreach (var item in request.PermissionIds)
                    {
                        var permisson = await _context.Permissions.FindAsync(new object[] { item });
                        foundPermissions.Add(permisson);
                    }
                    entity.Permissions = foundPermissions;
                }

                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            return false;          
        }
    }
}

