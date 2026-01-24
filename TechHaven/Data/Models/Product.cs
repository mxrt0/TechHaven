using System.ComponentModel.DataAnnotations;
using static TechHaven.Common.EntityValidation.Product;
namespace TechHaven.Data.Models;

public class Product
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(NameMaxLength)]
    public string Name { get; set; } = null!;

    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    [Required]
    [MaxLength(DescriptionMaxLength)]
    public string Description { get; set; } = null!;

    [Required]
    [MaxLength(SpecsJsonMaxLength)]
    public string SpecsJson { get; set; } = null!;

    public decimal Price { get; set; }

    public int StockQuantity { get; set; }

    [Required]
    [MaxLength(ImageUrlMaxLength)]
    public string ImageUrl { get; set; } = null!;

}
