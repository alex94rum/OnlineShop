using Microsoft.AspNetCore.Mvc;
using OnlineShop.Interfaces;
using OnlineShop.Repositories;

namespace OnlineShop.Controllers;

public class HomeController : Controller
{
    private readonly IProductsRepository _productsRepository;

    public HomeController(IProductsRepository productsRepository)
    {
        _productsRepository = productsRepository;
    }

    public IActionResult Index()
    {
        var products = _productsRepository.GetAll();

        return View(products);
    }
}