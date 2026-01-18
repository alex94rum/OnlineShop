using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Helpers;

namespace OnlineShop.Controllers;

public class FavoriteController : Controller
{
    private readonly IProductsRepository _productsRepository;
    private readonly IFavoritesRepository _favoritesRepository;

    public FavoriteController(IProductsRepository productsRepository, IFavoritesRepository favoritesRepository)
    {
        _productsRepository = productsRepository;
        _favoritesRepository = favoritesRepository;
    }

    public IActionResult Index()
    {
        var favorite = _favoritesRepository.TryGetByUserId(Constants.UserId);

        return View(favorite.ToFavoriteViewModel());
    }

    public IActionResult Add(int productId)
    {
        var product = _productsRepository.TryGetById(productId);

        if (product != null)
        {
            _favoritesRepository.Add(product, Constants.UserId);
        }

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int productId)
    {
        _favoritesRepository.Delete(productId, Constants.UserId);

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Clear()
    {
        _favoritesRepository.Clear(Constants.UserId);

        return RedirectToAction(nameof(Index));
    }
}
