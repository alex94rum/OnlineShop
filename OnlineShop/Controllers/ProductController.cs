using Microsoft.AspNetCore.Mvc;
using OnlineShop.Interfaces;

namespace OnlineShop.Controllers;

public class ProductController : Controller
{
    private readonly IProductsRepository _productsRepository;

    public ProductController(IProductsRepository productsRepository)
    {
        _productsRepository = productsRepository;
    }

    public IActionResult Index(int id)
    {
        var product = _productsRepository.TryGetById(id);

        return View(product);
    }
}