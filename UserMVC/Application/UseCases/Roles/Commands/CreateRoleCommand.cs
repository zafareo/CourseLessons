using Application.Common.Abstraction;
using AutoMapper;
using Domain.IdentityEntities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MediatrEntities.Roles.Commands
{
    public class CreateRoleCommand : IRequest<Guid>
    {
        public string Name { get; init; }
        public List<Guid>? PermissionIds { get; init; }
    }
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public CreateRoleCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService, IMapper mapper)
               => (_context, _currentUserService, _mapper) = (context, currentUserService, mapper);


        public async Task<Guid> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {



            var entity = new Role
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = _currentUserService.Name
            };

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
            await _context.Roles.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }

}
