using Identity.DTOs.User;
using Identity.Entities;

namespace Identity.Services.Interfaces
{
    public interface IUserService
    {

        Task<UserResponseDto> GetByIdAsync(int userId);

        Task<List<UserResponseDto>> GetAllAsync();

        Task<UserResponseDto> GetUserWithRolesAsync(int userId);


        Task<List<string>> GetUserRolesAsync(int userId);

        Task<List<string>> GetUserPermissionsAsync(int userId);

        Task<UserResponseDto> CreateUserAsync(CreateUserDto dto);

        Task<UserResponseDto> UpdateUserAsync(int id, UpdateUserDto dto);
        Task DeleteUserAsync(int userId);
    }
}
