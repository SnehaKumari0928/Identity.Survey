using AutoMapper;
using Identity.DTOs.Permission;
using Identity.DTOs.Role;
using Identity.Entities;

namespace Identity.Mappings
{
    public class PermissionMappingProfile: Profile
    {

        public PermissionMappingProfile() 
        {
            CreateMap<CreatePermissionDto, Permission>()
              .ForMember(dest =>
              dest.PermissionId, opt => opt.Ignore())
              .ForMember(dest =>
              dest.UserPermissions, opt => opt.Ignore())
              .ForMember(dest =>
              dest.RolePermissions, opt => opt.Ignore());


            CreateMap<Permission, PermissionDto>();
          
        }
    }
}
