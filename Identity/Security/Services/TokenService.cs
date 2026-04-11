using Identity.Entities;
using Identity.Repositories.Interfaces;
using Identity.Security.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Identity.Security.Services
{
    public class TokenService
    {
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepo;

        public TokenService(IConfiguration config, IUserRepository userRepo)
        {
            _config = config;
            _userRepo = userRepo;
        }

        public async Task<string> GenerateAccessTokenAsync(User user)
        {
            var claims = new List<Claim>
            {
                new
                Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var roles = await _userRepo.GetUserRolesAsync(user.UserId);

            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var permissions = await _userRepo.GetUserPermissionsAsync(user.UserId);

            foreach(var permission in permissions)
            {
                claims.Add(new Claim("Permission", permission));

               
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    Convert.ToDouble(_config["Jwt:AccessTokenExpiryMinutes"])
                    ),
                 signingCredentials: cred
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public string GenerateRefreshToken()
        {
            var randomBytes = new byte[64];

            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);

            return Convert.ToBase64String(randomBytes);
        }
    }
}
