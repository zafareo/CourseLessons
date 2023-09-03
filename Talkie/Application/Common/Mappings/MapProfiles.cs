using Application.Common.DTO;
using AutoMapper;
using Domain.Entities;
using Domain.IdentityEntities;

namespace Application.Common.Mappings
{
    public class MapProfiles : Profile
    {
        public MapProfiles()
        {
            CreateMap<CommentGetDTO, Comment>().ReverseMap();
            CreateMap<PermissionGetDTO, Permission>().ReverseMap();
            CreateMap<PostGetDTO, Post>().ReverseMap();
            CreateMap<UserGetDTO, User>().ReverseMap();
            CreateMap<RoleGetDTO, Role>().ReverseMap();
        }
    }
}
