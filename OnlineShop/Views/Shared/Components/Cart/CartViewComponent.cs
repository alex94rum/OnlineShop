using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Helpers;

namespace OnlineShop.Views.Shared.Components.Cart
{
    public class CartViewComponent(ICartsRepository cartsRepository) : ViewComponent
    {
        private readonly ICartsRepository _cartsRepository = cartsRepository;

        public IViewComponentResult Invoke()
        {
            var cart = _cartsRepository.TryGetByUserId(Constants.UserId);
            var productsCount = cart?.ToCartViewModel()?.Quantity ?? 0;

            return View("Cart", productsCount);
        }
    }
}
