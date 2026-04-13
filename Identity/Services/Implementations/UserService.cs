using AutoMapper;
using Identity.DTOs.User;
using Identity.Entities;
using Identity.Exceptions;
using Identity.Repositories.Interfaces;
using Identity.Services.Interfaces;

namespace Identity.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;
        
        public UserService(IUserRepository userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<UserResponseDto> GetByIdAsync(int userId)
        {
            var user = await _userRepo.GetByIdAsync(userId);

            if(user == null)
            {
                throw new NotFoundException("User not found");
            }

            return _mapper.Map<UserResponseDto>(user);
        }
        public async Task<List<UserResponseDto>> GetAllAsync()
        {
            var users = await _userRepo.GetUsersWithRolesAndPermissions();

            return _mapper.Map<List<UserResponseDto>>(users);
        }

        public async Task<UserResponseDto> CreateUserAsync(CreateUserDto dto)
        {
            var existingUser = await _userRepo.GetByEmailAsync(dto.Email);

            if(existingUser == null)
            {
                throw new BadRequestException("Email already exists");
            }

            var user = _mapper.Map<User>(dto);

            var createdUser = await _userRepo.CreateAsync(user);

            return _mapper.Map<UserResponseDto>(createdUser);

        }
        public async Task<UserResponseDto> UpdateUserAsync(int id, UpdateUserDto dto)
        {
            var existingUser = await _userRepo.GetByIdAsync(id);

            if(existingUser == null)
            {
                throw new NotFoundException("User not found");
            }

            _mapper.Map(dto, existingUser);

            await _userRepo.UpdateAsync(existingUser);

            return _mapper.Map<UserResponseDto>(existingUser);
        }
        public async Task DeleteUserAsync(int userId)
        {
            var existingUser = await _userRepo.GetByIdAsync(userId);

            if (existingUser == null)
            {
                throw new NotFoundException("User not found");
            }

            await _userRepo.DeleteAsync(userId);

        }

        public async Task<UserResponseDto> GetUserWithRolesAsync(int userId)
        {
            var user = await _userRepo.GetUserWithRolesAsync(userId);

            if(user == null)
            {
                throw new NotFoundException("User Not Found");
            }

            var dto = _mapper.Map<UserResponseDto>(user);

            dto.Roles = await _userRepo.GetUserRolesAsync(userId);
            dto.Permissions = await _userRepo.GetUserPermissionsAsync(userId);

            return dto;
        }


        public async Task<List<string>> GetUserRolesAsync(int userId)
        {

            var user = await _userRepo.GetByIdAsync(userId);

            if (user == null)
            {
                throw new NotFoundException("User not Found");
            }
            return await _userRepo.GetUserRolesAsync(userId);

        }

        public async Task<List<string>> GetUserPermissionsAsync(int userId)
        {
            var user = await _userRepo.GetByIdAsync(userId);

            if (user == null)
            {
                throw new NotFoundException("User not Found");
            }
            return await _userRepo.GetUserPermissionsAsync(userId);
        }
    }
}
