using ECommerce_MVC.Data.Interfaces;
using ECommerce_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce_MVC.Data.Repositories
{
    public class OrderService:IOrderService
    {
        private readonly DataContext _context;

        public OrderService(DataContext context)
        {
            _context = context;
        }
        public Task<List<Order>> GetOrdersByUserIdAsync(string UserId)
        {
            return _context.Orders.Include(o => o.OrderItems).ThenInclude(e => e.Product).Where(e => e.UserId == UserId).ToListAsync();
        }

        public async Task StoreOrderAsync(List<ShoppingCartItem> items, string UserId)
        {
            var order = new Order()
            {
                UserId = UserId,
            };
            await _context.AddAsync(order);
            await _context.SaveChangesAsync();
            foreach (var item in items)
            {
                var orderItem = new orderItem()
                {
                    Amount = item.Amount,
                    Price = item.Product.Price,
                    OrderId = order.Id,
                    ProductId = item.Product.Id
                };
                await _context.OrderItems.AddAsync(orderItem);
            }
            await _context.SaveChangesAsync();

        }
    }
}
