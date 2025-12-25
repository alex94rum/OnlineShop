using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace OnlineShop.Models;

public class Order
{
    public Guid Id { get; set; }


    [ValidateNever]
    [AllowNull]
    public string UserId { get; set; }


    [ValidateNever]
    [AllowNull]
    public List<CartItem> Items { get; set; }


    [Required]
    [AllowNull]
    public DeliveryUser DeliveryUser { get; set; }


    [Required]
    public DateTime CreationDateTime { get; set; }


    [Required]
    public OrderStatus Status { get; set; }


    public decimal? TotalCost => Items?.Sum(item => item.Cost);


    public int? ItemsQuantity => Items?.Sum(item => item.Quantity);
}