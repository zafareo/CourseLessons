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

namespace Application.MediatrEntities.Comments.Queries
{
    public class GetByIdComentQuery : IRequest<CommentGetDTO>
    {
        public Guid Id { get; set; }
    }

    public class GetByIdCommentQueryHandler : IRequestHandler<GetByIdComentQuery, CommentGetDTO>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetByIdCommentQueryHandler(IApplicationDbContext context, IMapper mapper)
               => (_context, _mapper) = (context, mapper);


        public async Task<CommentGetDTO> Handle(GetByIdComentQuery request, CancellationToken cancellationToken)
        {
            Comment? comment = await _context.Comments
                  .FindAsync(new object[] { request.Id }, cancellationToken);
            if (comment != null)
            {
                var result = _mapper.Map<CommentGetDTO>(comment);
                return result;
            }
            return null;          
        }
    }
}
