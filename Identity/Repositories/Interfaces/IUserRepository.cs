using Identity.Entities;

namespace Identity.Repositories.Interfaces
{
    public interface IUserRepository: IGenericRepository<User>

    {
       
        Task<User> GetUserWithRolesAsync(int userId);
        Task<List<string>> GetUserRolesAsync(int userId);

        Task<List<string>> GetUserPermissionsAsync(int userId);

        Task<List<User>> GetUsersWithRolesAndPermissions();

    }
}
