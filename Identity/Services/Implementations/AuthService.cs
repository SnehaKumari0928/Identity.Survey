using AutoMapper;
using Identity.DTOs.Auth;
using Identity.DTOs.User;
using Identity.Entities;
using Identity.Exceptions;
using Identity.Repositories.Interfaces;
using Identity.Security.Interfaces;
using Identity.Services.Interfaces;

namespace Identity.Services.Implementations
{
    public class AuthService: IAuthService
    {

        private readonly IAuthRepository _authRepo;
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;   

        public AuthService(IAuthRepository authRepo, IMapper mapper, ITokenService tokenService,IUserRepository userRepo)
        {
            _authRepo = authRepo;
            _mapper = mapper;
            _tokenService = tokenService;
            _userRepo = userRepo;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {
            var existingUser = await _userRepo.GetByIdAsync(dto.Email);

            if(existingUser != null)
            {
                throw new BadHttpRequestException("Email Already exists.");
            }

            var user = _mapper.Map<User>(dto);

            var createUser = await _authRepo.RegisterAsync(user);

            return await GenerateAuthResponse(createUser);
        }
        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var existingUser = await _userRepo.GetByEmailAsync(dto.Email);
            if (existingUser == null)
            {
                throw new DirectoryNotFoundException("User not found");
            }

            if(existingUser.HashedPassword != dto.Password)
            {
                throw new UnauthorizedAccessException("Invalid Password");
            }

            return await GenerateAuthResponse(existingUser);
            
        }

        public async Task<AuthResponseDto> RefreshTokenAsync(string refreshToken)
        {
            var existingToken = await _authRepo.GetRefreshTokenAsync(refreshToken);

            if (existingToken == null ||
                existingToken.IsRevoked || existingToken.ExpiryDate < DateTime.UtcNow)
            {
                throw new UnauthorizedException("Invalid refresh token");

            }

            var user = existingToken.User;

            await _authRepo.RevokeRefreshTokenAsync(refreshToken);

            return await GenerateAuthResponse(user);
        }

        public async Task RevokeRefreshTokenAsync(string refreshToken)
        {
            var token = await _authRepo.GetRefreshTokenAsync(refreshToken);

            if(token == null)
            {
                throw new NotFoundException("Refresh Token not found");
            }

            await _authRepo.RevokeRefreshTokenAsync(refreshToken);
        }

        private async Task<AuthResponseDto> GenerateAuthResponse(User user,bool isRefresh = false)
        {
            var accessToken = await _tokenService.GenerateAccessTokenAsync(user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            await _authRepo.AddRefreshTokenAsync(new RefreshToken
            {
                UserId = user.UserId,
                Token = refreshToken,
                ExpiryDate = DateTime.UtcNow.AddDays(7),
               IsRevoked = false
            });

            return new AuthResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                User =
               _mapper.Map<UserResponseDto>(user)
            };
        }

       
    }
}
