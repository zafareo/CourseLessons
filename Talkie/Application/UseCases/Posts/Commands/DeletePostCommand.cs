using Application.Common.Abstraction;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MediatrEntities.Posts.Commands
{
    public class DeletePostCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public DeletePostCommandHandler(IApplicationDbContext context)
               => _context = context;

        public async Task<bool> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Posts
                .FindAsync(new object[] { request.Id }, cancellationToken);
            if (entity != null)
            {
                _context.Posts.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            return false;   
        }
    }
}
