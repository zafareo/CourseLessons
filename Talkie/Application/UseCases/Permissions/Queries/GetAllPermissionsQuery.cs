using Application.Common.Abstraction;
using Application.Common.DTO;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MediatrEntities.Permissions.Queries
{
    public class GetAllPermissionsQuery :IRequest<IQueryable<PermissionGetDTO>>
    {
    }
    public class GetAllPermissionQueryHandler : IRequestHandler<GetAllPermissionsQuery, IQueryable<PermissionGetDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllPermissionQueryHandler(IApplicationDbContext context, IMapper mapper)
               => (_context, _mapper) = (context, mapper);

        public Task<IQueryable<PermissionGetDTO>> Handle(GetAllPermissionsQuery request, CancellationToken cancellationToken)
        {
            var entites = _context.Permissions;
            var result = _mapper.ProjectTo<PermissionGetDTO>(entites);

            return Task.FromResult(result);
        }
    }
}
