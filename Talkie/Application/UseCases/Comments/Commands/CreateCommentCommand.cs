using Application.Common.Abstraction;
using Domain.Entities;
using Domain.IdentityEntities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MediatrEntities.Comments.Commands
{
    public class CreateCommentCommand : IRequest<Guid>
    {
        public string Text { get; set; }
        public Guid PostId { get; set; }
    }

    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public CreateCommentCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
               => (_context, _currentUserService) = (context, currentUserService);



        public async Task<Guid> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var entity = new Comment
            {
                Id = Guid.NewGuid(),
                Text = request.Text,
                AuthorId = _currentUserService.Id,
                PostId = request.PostId,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = _currentUserService.Name

            };
            await _context.Comments.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;

        }
    }
}
