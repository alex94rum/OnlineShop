using Microsoft.AspNetCore.Mvc;
using OnlineShop.Repositories;

namespace OnlineShop.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            var cart = CartsRepository.TryGetByUserId(Constants.UserId);

            return View(cart);
        }

        public IActionResult Add(int productId)
        {
            var product = ProductsRepository.TryGetById(productId);

            if (product != null)
            {
                CartsRepository.Add(product, Constants.UserId);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
