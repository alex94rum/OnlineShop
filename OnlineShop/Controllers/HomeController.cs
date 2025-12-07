using Microsoft.AspNetCore.Mvc;
using OnlineShop.Repositories;

namespace OnlineShop.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        var products = ProductsRepository.GetAll();

        return View(products);
    }
}