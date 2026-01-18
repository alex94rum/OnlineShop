using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShop.Helpers;
using OnlineShop.Models;

namespace OnlineShop.Controllers;

public class OrderController : Controller
{
    private readonly ICartsRepository _cartsRepository;
    private readonly IOrdersRepository _ordersRepository;

    public OrderController(ICartsRepository cartsRepository, IOrdersRepository ordersRepository)
    {
        _cartsRepository = cartsRepository;
        _ordersRepository = ordersRepository;
    }

    public IActionResult Index()
    {
        var cart = _cartsRepository.TryGetByUserId(Constants.UserId);

        var order = new OrderViewModel()
        {
            Items = cart?.Items.ToCartItemViewModels()
        };

        return View(order);
    }

    [HttpPost]
    public IActionResult Buy(OrderViewModel order)
    {
        var cart = _cartsRepository.TryGetByUserId(Constants.UserId);

        if (cart == null)
        {
            return View(nameof(Index), order);
        }

        order.Items = cart.Items.ToCartItemViewModels();
        order.UserId = Constants.UserId;

        if (!ModelState.IsValid)
        {
            return View(nameof(Index), order);
        }

        var orderDb = new Order()
        {
            Id = order.Id,
            UserId = order.UserId,
            Items = cart.Items,
            DeliveryUser = order.DeliveryUser.ToDeliveryUserDb(),
            CreationDateTime = order.CreationDateTime,
            Status = (OrderStatus)order.Status,
        };

        _ordersRepository.Add(orderDb);

        _cartsRepository.Clear(Constants.UserId);

        return RedirectToAction(nameof(Success));
    }

    public IActionResult Success()
    {
        return View();
    }
}