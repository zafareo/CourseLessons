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
    public class CreatePostCommand : IRequest<Guid>
    {
        public string Name { get; init; }
        public string Content { get; init; }
        public Guid AuthorId { get; init; }
    }
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        public CreatePostCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
               => (_context, _currentUserService) = (context, currentUserService);

        public async Task<Guid> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var entity = new Post
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Content = request.Content,
                AuthorId = _currentUserService.Id,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = _currentUserService.Name
            };

            await _context.Posts.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }

}
