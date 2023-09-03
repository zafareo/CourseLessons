using Application.Common.Abstraction;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MediatrEntities.Comments.Commands
{
    public class DeleteCommentCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        public DeleteCommentCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Comments
                .FindAsync(new object[] { request.Id }, cancellationToken);
            if (entity is null)
                return false;
            else
            {
                _context.Comments.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
           
        }
    }

}
