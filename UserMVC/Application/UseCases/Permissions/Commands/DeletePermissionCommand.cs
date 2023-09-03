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
    public class DeletePermissionCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
    public class DeletePermissionCommandHandler : IRequestHandler<DeletePermissionCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public DeletePermissionCommandHandler(IApplicationDbContext context)
               => _context = context;



        public async Task<bool> Handle(DeletePermissionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Permissions.FindAsync(new object[] { request.Id }, cancellationToken);
            if (entity != null)
            { 
                _context.Permissions.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            return false;         
        }
    }
}
