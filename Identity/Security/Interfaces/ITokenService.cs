using Identity.Entities;

namespace Identity.Security.Interfaces
{
    public class ITokenService
    {

        Task<string> GenerateAccessTokenAsync(User user);
        string GenerateRefreshToken();
    }
}
