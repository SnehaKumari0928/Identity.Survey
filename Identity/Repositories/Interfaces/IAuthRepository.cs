using Identity.Entities;

namespace Identity.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task AddRefreshTokenAsync(RefreshToken token);
        Task<RefreshToken> GetRefreshTokenAsync(string token);
        Task RevokeRefreshTokenAsync(string token);
    }
}
