using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Helpers;
using Microsoft.AspNetCore.Http;

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

        return View(products.ToProductViewModels());
    }

    public IActionResult Search(string query)
    {
        if (query == null)
        {
            return View();
        }

        var products = _productsRepository.Search(query);

        return View(products.ToProductViewModels());
    }

    [HttpPost]
    public IActionResult ChangeLanguage(string culture, string returnUrl)
    {
        Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddDays(7),
            }
        );

        return LocalRedirect(returnUrl);
    }
}