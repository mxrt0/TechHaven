using System.ComponentModel.DataAnnotations;

namespace TechHaven.Data.Models;

public class Order
{
    public Guid Id { get; set; }

    [Required]
    public string UserId { get; set; } = null!;
    public ApplicationUser User { get; set; } = null!;

    public DateTime OrderDate { get; set; }

    public virtual ICollection<ProductSale> OrderItems { get; set; } = new List<ProductSale>();
}
