using Application.Common.Abstraction;
using Application.Common.DTO;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MediatrEntities.Users.Queries
{
    public class GetAllUsersQuery : IRequest<IQueryable<UserGetDTO>>
    {

    }
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IQueryable<UserGetDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(IApplicationDbContext context, IMapper mapper)
        => (_context, _mapper) = (context, mapper);

        public  Task<IQueryable<UserGetDTO>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var entities = _context.Users.Include(x => x.Roles);
            var result = _mapper.ProjectTo<UserGetDTO>(entities);
            return (Task<IQueryable<UserGetDTO>>)result;
        }
    }
}
