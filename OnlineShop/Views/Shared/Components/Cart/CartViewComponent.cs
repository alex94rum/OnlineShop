using Microsoft.AspNetCore.Mvc;
using OnlineShop.Interfaces;

namespace OnlineShop.Views.Shared.Components.Cart
{
    public class CartViewComponent(ICartsRepository cartsRepository) : ViewComponent
    {
        private readonly ICartsRepository _cartsRepository = cartsRepository;

        public IViewComponentResult Invoke()
        {
            var cart = _cartsRepository.TryGetByUserId(Constants.UserId);
            var productsCount = cart?.Quantity ?? 0;

            return View("Cart", productsCount);
        }
    }
}
