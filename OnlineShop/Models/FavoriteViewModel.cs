namespace OnlineShop.Models;

public class FavoriteViewModel
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public List<ProductViewModel> Items { get; set; }
}
