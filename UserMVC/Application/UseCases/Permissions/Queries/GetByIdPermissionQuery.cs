using Application.Common.Abstraction;
using Application.Common.DTO;
using AutoMapper;
using Domain.IdentityEntities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MediatrEntities.Permissions.Queries
{
    public class GetByIdPermissionQuery : IRequest<PermissionGetDTO>
    {
        public Guid Id { get; init; }
    }
    public class GetByIdPermissionQueryHandler : IRequestHandler<GetByIdPermissionQuery, PermissionGetDTO>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetByIdPermissionQueryHandler(IApplicationDbContext context, IMapper mapper)
               => (_context, _mapper) = (context, mapper);

        public async Task<PermissionGetDTO> Handle(GetByIdPermissionQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Permissions.FindAsync(new object[] { request.Id }, cancellationToken);
            if (entity != null)
            { 
                var result = _mapper.Map<PermissionGetDTO>(entity);
                return result;
            }
            return null;
        }
    }
}
