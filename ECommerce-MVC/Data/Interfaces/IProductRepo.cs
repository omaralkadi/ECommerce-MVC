using ECommerce_MVC.Models;
using System.Linq.Expressions;

namespace ECommerce_MVC.Data.Interfaces
{
    public interface IProductRepo : IGenericRepo<Product>
    {
        public Task<IEnumerable<Product>> getAllAsyncByName(Expression<Func<Product, bool>> expression);

    }
}
