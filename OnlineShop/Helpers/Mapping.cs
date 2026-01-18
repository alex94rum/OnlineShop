using OnlineShop.Db.Models;
using OnlineShop.Models;

namespace OnlineShop.Helpers;

public static class Mapping
{
    #region Product
    public static List<ProductViewModel> ToProductViewModels(this List<Product> productsDb)
    {
        var productsViewModel = new List<ProductViewModel>();

        foreach (var productDb in productsDb)
        {
            productsViewModel.Add(productDb.ToProductViewModel());
        }

        return productsViewModel;
    }

    public static ProductViewModel ToProductViewModel(this Product productDb)
    {
        return new ProductViewModel()
        {
            Id = productDb.Id,
            Name = productDb.Name,
            Cost = productDb.Cost,
            Description = productDb.Description,
            PhotoPath = productDb.PhotoPath
        };
    }

    public static Product ToProductDb(this ProductViewModel product)
    {
        return new Product()
        {
            Id = product.Id,
            Name = product.Name,
            Cost = product.Cost,
            Description = product.Description,
            PhotoPath = product.PhotoPath,
        };
    }
    #endregion


    #region Cart
    public static List<CartItemViewModel> ToCartItemViewModels(this List<CartItem> cartDbItems)
    {
        var cartItemsViewModel = new List<CartItemViewModel>();

        foreach (var cartDbItem in cartDbItems)
        {
            cartItemsViewModel.Add(cartDbItem.ToCartItemViewModel());
        }

        return cartItemsViewModel;
    }

    public static CartItemViewModel ToCartItemViewModel(this CartItem cartDbItem)
    {
        return new CartItemViewModel()
        {
            Id = cartDbItem.Id,
            Product = cartDbItem.Product.ToProductViewModel(),
            Quantity = cartDbItem.Quantity,
        };
    }

    public static CartViewModel? ToCartViewModel(this Cart? cartDb)
    {
        if (cartDb == null)
        {
            return null;
        }

        return new CartViewModel()
        {
            Id = cartDb.Id,
            UserId = cartDb.UserId,
            Items = cartDb.Items.ToCartItemViewModels(),
        };
    }
    #endregion

    #region Comparison
    public static ComparisonViewModel? ToComparisonViewModel(this Comparison? comparisonDb)
    {
        if (comparisonDb == null)
        {
            return null;
        }

        return new ComparisonViewModel()
        {
            Id = comparisonDb.Id,
            UserId = comparisonDb.UserId,
            Items = comparisonDb.Items.ToProductViewModels()
        };
    }
    #endregion

    #region Favorite
    public static FavoriteViewModel? ToFavoriteViewModel(this Favorite? favoriteDb)
    {
        if (favoriteDb == null)
        {
            return null;
        }

        return new FavoriteViewModel()
        {
            Id = favoriteDb.Id,
            UserId = favoriteDb.UserId,
            Items = favoriteDb.Items.ToProductViewModels()
        };
    }
    #endregion
}