using ECommerce_MVC.Data.Interfaces;
using ECommerce_MVC.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ECommerce_MVC.Data.Repositories
{
    public class ProductRepo : GenericRepo<Product>, IProductRepo
    {
        private readonly DataContext _context;

        public ProductRepo(DataContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Product>> getAllAsyncByName(Expression<Func<Product, bool>> expression)
        {
            return await _context.Set<Product>().Include(e => e.Category).Where(expression).ToListAsync();
        }
    }
}
