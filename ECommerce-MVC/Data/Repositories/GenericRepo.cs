using ECommerce_MVC.Data.Interfaces;
using ECommerce_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce_MVC.Data.Repositories
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        public DataContext _context { get; }

        public GenericRepo(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> getAllAsync()
        {
            if (typeof(T) == typeof(Product))
            {
                return (IEnumerable<T>)await _context.Set<Product>().Include(x => x.Category).ToListAsync();

            }
            return await _context.Set<T>().ToListAsync();

        }

        public async Task<T> getByIdAsync(int id)
        {
            if (typeof(T) == typeof(Product))
            {
                return await _context.Set<Product>().Include(c=>c.Category).FirstOrDefaultAsync(x=>x.Id==id) as T;

            }
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<int> CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return await _context.SaveChangesAsync();
        }
    }
}
