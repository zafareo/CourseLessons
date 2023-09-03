using Application.Common.Abstraction;
using Application.Common.DTO;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MediatrEntities.Posts.Queries
{
    public class GetAllPostsQuery : IRequest<IQueryable<PostGetDTO>>
    {
    }
    public class GetAllPostQueryHandler : IRequestHandler<GetAllPostsQuery, IQueryable<PostGetDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllPostQueryHandler(IApplicationDbContext context, IMapper mapper)
               => (_context, _mapper) = (context, mapper);


        public async Task<IQueryable<PostGetDTO>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
        {
            var entities = _context.Posts;
            var result = _mapper.ProjectTo<PostGetDTO>(entities);
            return await Task.FromResult(result);

        }
    }
}
