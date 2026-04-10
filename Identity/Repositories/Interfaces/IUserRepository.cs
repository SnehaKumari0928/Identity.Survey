using Identity.Entities;

namespace Identity.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByEmailAsync(string email);

        Task<User> GetByIdAsync(string email);

        Task<User> GetUserWithRolesAsync(int userId);
        Task<List<string>> GetUserRolesAsync(int userId);

        Task<List<string>> GetUserPermissionsAsync(int userId);

    }
}
