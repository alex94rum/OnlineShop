using Microsoft.AspNetCore.Mvc;
using OnlineShop.Interfaces;
using OnlineShop.Models;

namespace OnlineShop.Controllers;

public class AdminController(IProductsRepository productsRepository, IOrdersRepository ordersRepository) : Controller
{
    #region Orders
    public IActionResult Orders()
    {
        var orders = ordersRepository.GetAll();

        return View(orders);
    }

    public IActionResult DetailOrder(Guid orderId)
    {
        var order = ordersRepository.TryGetById(orderId);

        return View(order);
    }

    [HttpPost]
    public IActionResult UpdateOrderStatus(Guid orderId, OrderStatus status)
    {
        ordersRepository.UpdateStatus(orderId, status);

        return RedirectToAction(nameof(Orders));
    }

    #endregion

    public IActionResult Users()
    {
        return View();
    }

    public IActionResult Roles()
    {
        return View();
    }

    #region Products
    public IActionResult Products()
    {
        var products = productsRepository.GetAll();

        return View(products);
    }

    public IActionResult DeleteProduct(int id)
    {
        productsRepository.Delete(id);

        return RedirectToAction("Products");
    }

    public ActionResult AddProduct()
    {
        return View();
    }

    [HttpPost]
    public ActionResult AddProduct(Product product)
    {
        if (!ModelState.IsValid)
        {
            return View(product);
        }

        productsRepository.Add(product);

        return RedirectToAction("Products");
    }

    public ActionResult UpdateProduct(int id)
    {
        var product = productsRepository.TryGetById(id);

        return View(product);
    }

    [HttpPost]
    public ActionResult UpdateProduct(Product product)
    {
        if (!ModelState.IsValid)
        {
            return View(product);
        }

        productsRepository.Update(product);

        return RedirectToAction("Products");
    }
    #endregion
}