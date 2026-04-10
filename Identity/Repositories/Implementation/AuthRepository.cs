using Identity.Data;
using Identity.Entities;
using Identity.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Identity.Repositories.Implementation
{
    public class AuthRepository: IAuthRepository
    {

        private readonly AppDbContext _context;

        public AuthRepository(AppDbContext context)
        {
            _context = context; 
        }

       public async Task<User> RegisterAsync(User user)
        {
             await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;

        }
        public async Task AddRefreshTokenAsync(RefreshToken token)
        {
            await _context.RefreshTokens.AddAsync(token);
            await _context.SaveChangesAsync();
        }
        public async Task<RefreshToken> GetRefreshTokenAsync(string token)
        {
            return await _context.RefreshTokens
                .Include(rt => rt.User)
                .FirstOrDefaultAsync(rt =>
                rt.Token == token && !rt.IsRevoked);
  
        }
        public async Task RevokeRefreshTokenAsync(string token)
        {
            var refreshToken = await _context.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.Token == token);

            if (refreshToken != null)
            {
                refreshToken.IsRevoked = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
