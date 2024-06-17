using ECommerce_MVC.Data.Interfaces;
using ECommerce_MVC.Models;

namespace ECommerce_MVC.Data.Repositories
{
    public class CategoryRepo : GenericRepo<Category>,ICategoryRepo
    {
        public CategoryRepo(DataContext context) : base(context)
        {

        }

    }
}
