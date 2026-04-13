using AutoMapper;
using Identity.DTOs.Role;
using Identity.Entities;

namespace Identity.Mappings
{
    public class RoleMappingProfile : Profile
    {

        public RoleMappingProfile() 
        {
            CreateMap<CreateRoleDto, Role>()
            .ForMember(dest =>
            dest.RoleId, opt => opt.Ignore())
            .ForMember(dest =>
            dest.UserRoles, opt => opt.Ignore())
            .ForMember(dest =>
            dest.RolePermissions, opt => opt.Ignore());

            CreateMap<UpdateRoleDto, Role>()
              .ForMember(dest =>
            dest.RoleId, opt => opt.Ignore())
            .ForMember(dest =>
            dest.UserRoles, opt => opt.Ignore())
            .ForMember(dest =>
            dest.RolePermissions, opt => opt.Ignore());

            CreateMap<Role, RoleResponseDto>()
            .ForMember(dest =>
            dest.Permissions,
            opt =>
            opt.MapFrom(src =>
            src.RolePermissions.Select(rp => rp.Permission.Name)))
            .ForMember(dest => 
            dest.Users,
            opt => opt.MapFrom(src=>
            src.UserRoles.Select(ur =>
            ur.User.Email)));
            

        }
    }
}
