using Application.Common.Abstraction;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MediatrEntities.Users.Commands
{
    public class DeleteUserCommand : IRequest<bool>
    {
        public Guid Id { get; init; }

    }
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public DeleteUserCommandHandler(IApplicationDbContext context)
            => _context = context;



        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Users
                .FindAsync(new object[] { request.Id }, cancellationToken);
            if (entity != null)
            {
                _context.Users.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return true;
            }
            return false;
        }
    }
}
