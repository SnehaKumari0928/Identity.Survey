namespace Identity.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {

        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(int id);

    }
}
