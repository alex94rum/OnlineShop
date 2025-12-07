using Microsoft.AspNetCore.Mvc;
using OnlineShop.Repositories;

namespace OnlineShop.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index(int id)
        {
            var product = ProductsRepository.TryGetById(id);

            return View(product);
        }
    }
}