using Application.Common.Abstraction;
using Application.Common.DTO;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MediatrEntities.Roles.Queries
{
    public class GetAllRolesQuery : IRequest<IQueryable<RoleGetDTO>>
    {
    }
    public class GetAllRoleQueryHandler : IRequestHandler<GetAllRolesQuery, IQueryable<RoleGetDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetAllRoleQueryHandler(IApplicationDbContext context, IMapper mapper)
              => (_context, _mapper) = (context, mapper);
        public async Task<IQueryable<RoleGetDTO>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            var entities = _context.Roles;
            var result = _mapper.ProjectTo<RoleGetDTO>(entities);
            return await Task.FromResult(result);
        }
    }
}
