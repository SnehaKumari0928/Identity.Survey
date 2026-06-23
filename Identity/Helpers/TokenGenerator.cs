using System.Security.Cryptography;

namespace Identity.Helpers
{
    public class TokenGenerator
    {

        public static string GenerateToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        }
    }
}
