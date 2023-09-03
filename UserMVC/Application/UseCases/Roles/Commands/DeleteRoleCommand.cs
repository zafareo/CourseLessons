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
    public class DeleteRoleCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public DeleteRoleCommandHandler(IApplicationDbContext context)
               => _context = context;

        public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Roles
                .FindAsync(new object[] { request.Id }, cancellationToken);
            if (entity != null)
            {
                _context.Roles.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            return false;
        }
    }
}
