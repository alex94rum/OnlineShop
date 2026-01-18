using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Helpers;

namespace OnlineShop.Controllers;

public class ComparisonController : Controller
{
    private readonly IProductsRepository _productsRepository;
    private readonly IComparisonsRepository _comparisonsRepository;

    public ComparisonController(IProductsRepository productsRepository, IComparisonsRepository comparisonsRepository)
    {
        _productsRepository = productsRepository;
        _comparisonsRepository = comparisonsRepository;
    }

    public IActionResult Index()
    {
        var comparison = _comparisonsRepository.TryGetByUserId(Constants.UserId);

        return View(comparison?.ToComparisonViewModel());
    }

    public IActionResult Add(int productId)
    {
        var product = _productsRepository.TryGetById(productId);

        if (product != null)
        {
            _comparisonsRepository.Add(product, Constants.UserId);
        }

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int productId)
    {
        _comparisonsRepository.Delete(productId, Constants.UserId);

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Clear()
    {
        _comparisonsRepository.Clear(Constants.UserId);

        return RedirectToAction(nameof(Index));
    }
}