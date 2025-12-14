using Microsoft.AspNetCore.Mvc;
using OnlineShop.Interfaces;
using OnlineShop.Repositories;

namespace OnlineShop.Controllers;

public class CartController : Controller
{
    private readonly IProductsRepository _productsRepository;
    private readonly ICartsRepository _cartsRepository;

    public CartController(IProductsRepository productsRepository, ICartsRepository cartsRepository)
    {
        _productsRepository = productsRepository;
        _cartsRepository = cartsRepository;
    }

    public IActionResult Index()
    {
        var cart = _cartsRepository.TryGetByUserId(Constants.UserId);

        return View(cart);
    }

    public IActionResult Add(int productId)
    {
        var product = _productsRepository.TryGetById(productId);

        if (product != null)
        {
            _cartsRepository.Add(product, Constants.UserId);
        }

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Subtract(int productId)
    {
        _cartsRepository.Subtract(productId, Constants.UserId);

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Clear()
    {
        _cartsRepository.Clear(Constants.UserId);

        return RedirectToAction(nameof(Index));
    }
}
