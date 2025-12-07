using OnlineShop.Models;

namespace OnlineShop.Repositories
{
    public static class CartsRepository
    {
        private static readonly List<Cart> _carts = [];

        public static Cart? TryGetByUserId(string userId)
        {
            return _carts.FirstOrDefault(cart => cart.UserId == userId);
        }

        public static void Add(Product product, string userId)
        {
            var existingCart = TryGetByUserId(userId);

            if (existingCart == null)
            {
                existingCart = new Cart()
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Items = new List<CartItem>()
                    {
                        new CartItem()
                        {
                            Id = Guid.NewGuid(),
                            Product = product,
                            Quantity = 1
                        }
                    }
                };
                _carts.Add(existingCart);
            }
            else
            {
                var existingCartItem = existingCart.Items.FirstOrDefault(item =>
                    item.Product.Id == product.Id);

                if (existingCartItem == null)
                {
                    var newCartItem = new CartItem()
                    {
                        Id = Guid.NewGuid(),
                        Product = product,
                        Quantity = 1
                    };
                    existingCart.Items.Add(newCartItem);
                }
                else
                {
                    existingCartItem.Quantity++;
                }
            }
        }
    }
}
