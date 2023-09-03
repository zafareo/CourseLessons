using Application.Common.Abstraction;
using Application.Common.DTO;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MediatrEntities.Comments.Queries
{
    public class GetAllComentQuery : IRequest<IQueryable<CommentGetDTO>>
    {

    }
    public class GetAllCommentQueryHandler : IRequestHandler<GetAllComentQuery, IQueryable<CommentGetDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetAllCommentQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IQueryable<CommentGetDTO>> Handle(GetAllComentQuery request, CancellationToken cancellationToken)
        {
            var entities = _context.Comments;
            var result = _mapper.ProjectTo<CommentGetDTO>(entities);
            return await Task.FromResult(result);
        }
    }
}
