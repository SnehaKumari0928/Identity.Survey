using Identity.Entities;

namespace Identity.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<User> RegisterAsync(User user);
        Task AddRefreshTokenAsync(RefreshToken token);
        Task<RefreshToken> GetRefreshTokenAsync(string token);
        Task RevokeRefreshTokenAsync(string token);
    }
}
