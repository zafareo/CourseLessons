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

namespace Application.MediatrEntities.Roles.Queries
{
    public class GetByIdRoleQuery : IRequest<RoleGetDTO>
    {
        public Guid Id { get; set; }    
    }
    public class GetByIdRoleQueryHandler : IRequestHandler<GetByIdRoleQuery, RoleGetDTO>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetByIdRoleQueryHandler(IApplicationDbContext context, IMapper mapper)
               => (_context, _mapper) = (context, mapper);


        public async Task<RoleGetDTO> Handle(GetByIdRoleQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Roles.FindAsync(new object[] { request.Id }, cancellationToken);
            if (entity != null)
            { 
                var result = _mapper.Map<RoleGetDTO>(entity);
                return result;
            }
            return null;
        }
    }

}
