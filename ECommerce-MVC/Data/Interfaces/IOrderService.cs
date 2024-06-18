using ECommerce_MVC.Models;

namespace ECommerce_MVC.Data.Interfaces
{
    public interface IOrderService
    {
        public Task<List<Order>> GetOrdersByUserIdAsync(string UserId);
        public Task StoreOrderAsync(List<ShoppingCartItem> items,string UserId);
    }
}
