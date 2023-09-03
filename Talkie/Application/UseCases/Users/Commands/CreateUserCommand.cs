using Application.Common.Abstraction;
using Domain.Entities;
using Domain.IdentityEntities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MediatrEntities.Users.Commands
{
    public class CreateUserCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Picture { get; set; }
        public string Password { get; set; }
        public List<Guid>? RoleIds { get; set; }
    }
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        public CreateUserCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var entity = new User
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email,
                Password =request.Password,
                Picture = request.Picture,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = request.Name

            };
            if (request.RoleIds is not null)
            {
                List<Role> foundRoles = new();

                foreach (var roleId in request.RoleIds)
                {
                    var role = await _context.Roles.FindAsync(new object[] { roleId });
                    foundRoles.Add(role);
                }
                entity.Roles = foundRoles;
            }

            await _context.Users.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }

    }
}
