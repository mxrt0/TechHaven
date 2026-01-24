using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechHaven.Data.Models;

public class WishlistItem
{
    public int Id { get; set; }

    [Required]
    [ForeignKey(nameof(User))]
    public string UserId { get; set; } = null!;
    public ApplicationUser User { get; set; } = null!;

    [ForeignKey(nameof(Product))]
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public DateTime AddedAt { get; set; }
}
