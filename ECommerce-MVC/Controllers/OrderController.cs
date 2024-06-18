using ECommerce_MVC.Data.Cart;
using ECommerce_MVC.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce_MVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly ShoppingCart _shoppingCart;
        private  IProductRepo _product { get; }
        public IOrderService _orderService { get; }

        public OrderController(IProductRepo product,ShoppingCart shoppingCart,IOrderService orderService)
        {
            _product = product;
            _shoppingCart = shoppingCart;
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            var UserId = "";
            var Orders=await _orderService.GetOrdersByUserIdAsync(UserId);
            return View(Orders);
        }

        public IActionResult ShoppingCart()
        {
            var item=_shoppingCart.GetAllShoppingCartItems();
            ViewBag.Total=_shoppingCart.GetCartPrice();
            return View(item);
        }
        public async Task<IActionResult> AddToCart(int id)
        {
            var item =await _product.getByIdAsync(id);
            if(item != null)
            {
                await _shoppingCart.AddItemToShoppingCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var item=await _product.getByIdAsync(id);
            if(item != null)
            {
                await _shoppingCart.RemoveItemFromShoppingCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> CompleteOrder()
        {
            var ShoppingCart = _shoppingCart.GetAllShoppingCartItems();
            string UserId = "";
            await _orderService.StoreOrderAsync(ShoppingCart, UserId);
            _shoppingCart.ClearShoppingCart();
            return View("CompleteOrder");
        }


    }
}






