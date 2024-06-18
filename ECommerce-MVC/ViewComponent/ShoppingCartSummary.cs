namespace ECommerce_MVC.ViewComponent
{
    using ECommerce_MVC.Data.Cart;
    using Microsoft.AspNetCore.Mvc;
    public class ShoppingCartSummary : ViewComponent
    {
        private readonly ShoppingCart _cart;

        public ShoppingCartSummary(ShoppingCart cart)
        {
            _cart = cart;
        }

        public IViewComponentResult Invoke()
        {
            var items = _cart.GetAmoutOfProdcuts();
            return View(items);
        }
    }
}
