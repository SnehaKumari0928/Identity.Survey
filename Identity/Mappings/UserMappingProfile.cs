using AutoMapper;
using Identity.DTOs.User;
using Identity.Entities;

namespace Identity.Mappings
{
    public class UserMappingProfile: Profile
    {

        CreateMap<User,UserResponseDto>()
            .ForMember(dest => dest.Name,)
    }
}
