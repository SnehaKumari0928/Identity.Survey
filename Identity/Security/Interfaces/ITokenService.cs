using Identity.Entities;

namespace Identity.Security.Interfaces
{
    public interface ITokenService
    {

        Task<string> GenerateAccessTokenAsync(User user);
        string GenerateRefreshToken();
    }
}
