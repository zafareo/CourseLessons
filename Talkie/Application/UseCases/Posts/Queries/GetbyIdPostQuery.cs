using Application.Common.Abstraction;
using Application.Common.DTO;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MediatrEntities.Posts.Queries
{
    public class GetbyIdPostQuery : IRequest<PostGetDTO>
    {
        public Guid Id { get; set; }
    }
    public class GetByIdPostQueryHandler : IRequestHandler<GetbyIdPostQuery, PostGetDTO>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetByIdPostQueryHandler(IApplicationDbContext context, IMapper mapper)
               => (_context, _mapper) = (context, mapper);


        public async Task<PostGetDTO> Handle(GetbyIdPostQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Posts.FindAsync(new object[] { request.Id }, cancellationToken);
            if (entity != null)
            {
                var result = _mapper.Map<PostGetDTO>(entity);
                return result;
            }
            return null;
        }
    }
}
