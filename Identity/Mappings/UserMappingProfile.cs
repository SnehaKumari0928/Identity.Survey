using AutoMapper;
using Identity.DTOs.Auth;
using Identity.DTOs.User;
using Identity.Entities;

namespace Identity.Mappings
{
    public class UserMappingProfile: Profile
    {

       public UserMappingProfile()
        {
            CreateMap<CreateUserDto, User>()

                  .ForMember(dest =>
                  dest.UserId, opt => opt.Ignore()
                  )

                  .ForMember(dest =>
                  dest.HashedPassword,
                  opt => opt.MapFrom(src => src.Password))

                  .ForMember(dest =>
                  dest.IsActive,
                  opt => opt.MapFrom(_ => true))

                  .ForMember(dest =>
                  dest.IsDeleted, opt => opt.MapFrom(_ => false))

                  .ForMember(dest =>
                  dest.Profile, opt => opt.Ignore()
                  )
                  .ForMember(dest =>
                  dest.UserRoles, opt => opt.Ignore()
                  )
                   .ForMember(dest =>
                  dest.UserPermissions, opt => opt.Ignore()
                  )
                  .ForMember(dest =>
                  dest.RefreshTokens, opt => opt.Ignore()
                  );

            CreateMap<UpdateUserDto, User>()
                .ForMember(dest =>
                dest.UserId, opt => opt.Ignore())
                 .ForMember(dest =>
                  dest.Profile, opt => opt.Ignore()
                  )
                  .ForMember(dest =>
                  dest.UserRoles, opt => opt.Ignore()
                  )
                   .ForMember(dest =>
                  dest.UserPermissions, opt => opt.Ignore()
                  )
                  .ForMember(dest =>
                  dest.RefreshTokens, opt => opt.Ignore()
                  );

            CreateMap<User, UserResponseDto>()
                .ForMember(dest =>
                dest.Roles,
                opt => opt.MapFrom(src =>
                src.UserRoles.Select(ur => ur.Role.Name)))
                .ForMember(dest =>
                dest.Permissions,
                opt => opt.MapFrom(src =>
                src.UserPermissions.Select(up => up.Permission.Name)))
                .ForSourceMember(src =>
                 src.HashedPassword, opt =>
                 opt.DoNotValidate());
        }
    }
}
