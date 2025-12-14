using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;

namespace OnlineShop.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Authorization()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Authorization(Authorization authorization)
        {
            return View();
        }

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(Registration registration)
        {
            return View();
        }
    }
}
