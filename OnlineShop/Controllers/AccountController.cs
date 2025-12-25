using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;

namespace OnlineShop.Controllers;

public class AccountController : Controller
{
    public IActionResult Authorization()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Authorization(Authorization authorization)
    {
        if (authorization.Login == authorization.Password)
        {
            ModelState.AddModelError("",
                "Имя и пароль не должны совпадать");
        }

        if (!ModelState.IsValid)
        {
            return View(authorization);
        }

        return RedirectToAction(nameof(Index), "Home");
    }

    public IActionResult Registration()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Registration(Registration registration)
    {
        if (registration.Login == registration.Password)
        {
            ModelState.AddModelError("",
                "Имя и пароль не должны совпадать");
        }

        if (!ModelState.IsValid)
        {
            return View(registration);
        }

        return RedirectToAction(nameof(Index), "Home");
    }
}