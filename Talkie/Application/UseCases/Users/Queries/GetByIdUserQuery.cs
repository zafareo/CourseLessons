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

namespace Application.MediatrEntities.Users.Queries
{
    public class GetByIdUserQuery : IRequest<UserGetDTO>
    {
        public Guid UserId { get; init; }
    }
    public class GetByIdQueryHandler : IRequestHandler<GetByIdUserQuery, UserGetDTO>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;


        public GetByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
                => (_context, _mapper) = (context, mapper);

        public async Task<UserGetDTO> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Users.FindAsync(new object[] { request.UserId }, cancellationToken);

            if (entity != null)
            {
                var result = _mapper.Map<UserGetDTO>(entity);
                return result;
            }
            return null;
         
        }
    }
}
