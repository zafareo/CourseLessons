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
    public class UpdatePostCommand : IRequest<bool>
    {
        public Guid PostId { get; init; }
        public string Name { get; init; }
        public string Content { get; init; }
    }
    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        public UpdatePostCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
               => (_context, _currentUserService) = (context, currentUserService);

        public async Task<bool> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Posts
                .FindAsync(new object[] { request.PostId }, cancellationToken);
            if (entity != null)
            {
                var properties = typeof(UpdatePostCommand).GetProperties();
                foreach (var property in properties)
                {
                    var requestValue = property.GetValue(request);
                    if (requestValue is not null)
                    {
                        var entityProperty = entity.GetType().GetProperty(property.Name);
                        entityProperty.SetValue(entity, requestValue);
                    }
                }

                entity.LastUpdatedDate = DateTime.UtcNow;
                entity.LastUpdatedBy = _currentUserService.Name;
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            return false;            
        }
    }
}
