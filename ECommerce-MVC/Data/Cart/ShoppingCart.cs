using ECommerce_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce_MVC.Data.Cart
{
    public class ShoppingCart
    {
        private readonly DataContext _context;
        public string ShoppingCartId { get; set; }

        public ShoppingCart(DataContext context)
        {
            _context = context;

        }
        public static ShoppingCart getShoppingCart(IServiceProvider service)
        {
            var session = service.GetRequiredService<IHttpContextAccessor>().HttpContext.Session;
            var context = service.GetRequiredService<DataContext>();
            string CartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", CartId);
            var cart = new ShoppingCart(context)
            {
                ShoppingCartId = CartId // Set the ShoppingCartId property here
            };

            return cart;
        }

        public List<ShoppingCartItem> GetAllShoppingCartItems()
        {
            return _context.ShoppingCartItems.Where(e => e.ShoppingCartId == ShoppingCartId).Include(e => e.Product).ToList();
        }

        public int GetAmoutOfProdcuts()
        {
            return _context.ShoppingCartItems.Where(e => e.ShoppingCartId == ShoppingCartId).Select(e => e.Amount).Sum();
        }
        public decimal GetCartPrice()
        {
            return _context.ShoppingCartItems.Where(e => e.ShoppingCartId == ShoppingCartId).Select(e => e.Product.Price * e.Amount).Sum();
        }
        public async Task AddItemToShoppingCart(Product product)
        {
            var ShoppingCartItem = await _context.ShoppingCartItems.FirstOrDefaultAsync(e => e.ShoppingCartId == ShoppingCartId && e.Product.Id == product.Id);

            if (ShoppingCartItem is null)
            {
                var ShoppingCart = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    Product = product,
                    Amount = 1,
                };

                await _context.ShoppingCartItems.AddAsync(ShoppingCart);

            }
            else
            {
                ShoppingCartItem.Amount += 1;

            }
            await _context.SaveChangesAsync();
        }

        public async Task RemoveItemFromShoppingCart(Product product)
        {
            var ShoppingCartItem = await _context.ShoppingCartItems.FirstOrDefaultAsync(e => e.ShoppingCartId == ShoppingCartId && e.Product.Id == product.Id);
            if (ShoppingCartItem != null)
            {
                if (ShoppingCartItem.Amount > 1)
                {
                    ShoppingCartItem.Amount -= 1;

                }
                else
                {
                    _context.ShoppingCartItems.Remove(ShoppingCartItem);
                }
                await _context.SaveChangesAsync();
            }
        }

        public void ClearShoppingCart()
        {
            var ShoppingCart = _context.ShoppingCartItems.Where(e => e.ShoppingCartId == ShoppingCartId).ToList();
            _context.ShoppingCartItems.RemoveRange(ShoppingCart);
            _context.SaveChanges();
            
        }

    }
}
