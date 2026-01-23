namespace TechHaven.Data.Models;

public class Order
{
    public Guid Id { get; set; }

    public DateTime OrderDate { get; set; }

    public virtual ICollection<ProductSale> OrderItems { get; set; } = new List<ProductSale>();
}
