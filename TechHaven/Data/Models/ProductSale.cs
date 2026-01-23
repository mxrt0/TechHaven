using System.ComponentModel.DataAnnotations.Schema;

namespace TechHaven.Data.Models;

public class ProductSale
{
    [ForeignKey(nameof(Order))]
    public Guid OrderId { get; set; }

    public virtual Order Order { get; set; } = null!;

    [ForeignKey(nameof(Product))]
    public int ProductId { get; set; }

    public virtual Product Product { get; set; } = null!;

    public int Quantity { get; set; }
}
