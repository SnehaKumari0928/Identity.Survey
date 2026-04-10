using Identity.DTOs.User;
using Identity.Entities;

namespace Identity.Services.Interfaces
{
    public interface IUserService
    {

        Task<UserResponseDto> GetByIdAsync(int userId);
        Task<List<UserResponseDto>> GetAllAsync();

        Task<UserResponseDto> UpdateUserDto(int id, UpdateUserDto dto);
        Task DeleteUserAsync(int userId);
    }
}
