using Identity.DTOs.User;
using Identity.Repositories.Interfaces;
using Identity.Services.Interfaces;

namespace Identity.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        
        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<UserResponseDto> GetByIdAsync(int userId)
        {
            const UserResponseDto = await _userRepo.GetById
        }
        public async Task<List<UserResponseDto>> GetAllAsync()
        {

        }

        public async Task<UserResponseDto> UpdateUserDto(int id, UpdateUserDto dto)
        {

        }
        public async Task DeleteUserAsync(int userId)
        {

        }
    }
}
