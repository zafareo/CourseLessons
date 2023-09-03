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
    public class UpdateUserCommand : IRequest<bool>
    {
        public Guid Id { get; init; }
        public string? Name { get; init; }
        public string? Email { get; init; }
        public string? Picture { get; init; }
        public string? Password { get; init; }
        public Guid[]? RoleIds { get; init; }

    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        public UpdateUserCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {

            var entity = await _context.Users
                .FindAsync(new object[] { request.Id }, cancellationToken);
            if (entity != null)
            {
                var properties = typeof(UpdateUserCommand).GetProperties();
                foreach (var property in properties)
                {

                    var requestValue = property.GetValue(request);
                    if (requestValue is not null && (property.Name is not "RoleIds"))
                    {
                        var entityProperty = entity.GetType().GetProperty(property.Name);
                        entityProperty.SetValue(entity, requestValue);

                    }

                }

                entity.LastUpdatedBy = request.Name;
                entity.LastUpdatedDate = DateTime.Now;
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

                await _context.SaveChangesAsync(cancellationToken);

                return true;
            }
            return false;
        }
    }
}
