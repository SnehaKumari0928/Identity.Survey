using Identity.Data;
using Identity.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Identity.Repositories.Implementation
{
    public class GenericRepository<T>:
        IGenericRepository<T> where T: class
    {
        protected readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>()
                .ToListAsync();
        }
        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
        public void Delete(int id)
        {
            var entity = _context.Set<T>().Find(id);

            _context.Set<T>().Remove(entity);
        }
    }
}
