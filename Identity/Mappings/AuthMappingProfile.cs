using AutoMapper;
using Identity.DTOs.Auth;
using Identity.DTOs.User;
using Identity.Entities;

namespace Identity.Mappings
{
    public class AuthMappingProfile: Profile
    {
        public AuthMappingProfile() 
        {
            CreateMap<RegisterDto, User>()

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


            CreateMap<User, UserResponseDto>();


        }
    }
}
