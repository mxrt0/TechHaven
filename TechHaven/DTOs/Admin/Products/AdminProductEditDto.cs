using System.ComponentModel.DataAnnotations;
using static TechHaven.Common.EntityValidation.Product;
namespace TechHaven.DTOs.Admin.Products;

public class AdminProductEditDto
{
    public int Id { get; set; }

    [Required]
    [StringLength(NameMaxLength)]
    [Display(Name = "Product Name")]
    public string Name { get; set; } = null!;

    [Required]
    [StringLength(DescriptionMaxLength)]
    public string Description { get; set; } = null!;

    [Required]
    [Range(0.01, 9999.99, ErrorMessage = "Price must be greater than zero and less than 10000.")]
    public decimal Price { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Stock quantity cannot be negative.")]
    [Display(Name = "Stock Quantity")]
    public int StockQuantity { get; set; }

    [Required]
    [StringLength(ImageUrlMaxLength)]
    [Display(Name = "Image URL")]
    [RegularExpression("^[\\w./-]+\\.(?:jpg|jpeg|png|gif|webp|bmp)$", ErrorMessage = "Unsupported or invalid file format.")]
    public string ImageUrl { get; set; } = null!;
    public bool IsActive { get; set; }

    [Required(ErrorMessage = "Specs are required.")]
    public string SpecsJson { get; set; } = null!;

    public int CategoryId { get; set; }
    public string? CategoryName { get; set; }
}

