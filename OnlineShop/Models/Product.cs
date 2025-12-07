namespace OnlineShop.Models
{
    public class Product(int id, string name, decimal cost, string? description)
    {
        public int Id { get; } = id;
        public string Name { get; } = name;
        public decimal Cost { get; } = cost;
        public string? Description { get; } = description;
        public string? PhotoPath { get; } = "/img/anyProduct.png";

        public override string ToString() => $"{this.Id}{Environment.NewLine}{this.Name}{Environment.NewLine}{this.Cost:c}";
    }
}
