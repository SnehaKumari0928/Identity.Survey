using Identity.Entities;

namespace Identity.Repositories.Interfaces
{
    public interface IPasswordResetTokenRepository
    {

        Task AddAsync(PasswordResetToken token);
        Task<PasswordResetToken?> GetByTokenAsync(string token);
    }
}
