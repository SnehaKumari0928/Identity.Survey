using AutoMapper;
using Identity.DTOs.Auth;
using Identity.Entities;
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
            var existingUser = await _userRepo.GetByEmailAsync(dto.Email);

            if(existingUser != null)
            {
                throw new BadHttpRequestException("Email Already exists.");
            }

            var user = _mapper.Map<User>(dto);

            var createUser = await 
        }
        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {

        }

        public async Task<AuthResponseDto> RefreshTokenAsync(string refreshToken)
        {

        }

        private async Task<AuthResponseDto> GenerateAuthResponse(User user, bool isRefresh = false)
        {
            var accessToken = _tokenService.GenerateAccessTokenAsync(user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            
        }
    }
}
