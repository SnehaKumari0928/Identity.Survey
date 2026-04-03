using Identity.DTOs.Auth;

namespace Identity.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
        Task<AuthResponseDto> LoginAsync(LoginDto dto);

        Task<AuthResponseDto> RefreshTokenAsync(string refreshToken);
    }
}
