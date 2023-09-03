using Application.Common.Abstraction;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MediatrEntities.Comments.Commands
{
    public class UpdateCommentCommand : IRequest<bool>
    {
        public Guid Id { get; init; }
        public string? Text { get; init; }
    }

    public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        private readonly ICurrentUserService _currentUserService;

        public UpdateCommentCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
               => (_context, _currentUserService) = (context, currentUserService);


        public async Task<bool> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Comments
                .FindAsync(new object[] { request.Id }, cancellationToken);
            if (entity != null)
            {
                entity.AuthorId = _currentUserService.Id;
                entity.Text = request.Text;
                entity.LastUpdatedDate = DateTime.UtcNow;
                entity.LastUpdatedBy = _currentUserService.Name;

                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            return false;
        }
    }

}
