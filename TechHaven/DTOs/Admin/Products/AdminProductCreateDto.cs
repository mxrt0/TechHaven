namespace TechHaven.DTOs.Admin.Products;

using System.ComponentModel.DataAnnotations;
using static TechHaven.Common.EntityValidation.Product;

public class AdminProductCreateDto
{
    [Required]
    [StringLength(NameMaxLength)]
    [Display(Name = "Product Name")]
    public string Name { get; set; } = null!;

    [Required]
    [StringLength(DescriptionMaxLength)]
    [Display(Name = "Description")]
    public string Description { get; set; } = null!;

    [Required(ErrorMessage = "Specs are required.")]
    public string SpecsJson { get; set; } = null!;

    [Required]
    [Range(0.01, 9999, ErrorMessage = "Price must be greater than zero.")]
    [Display(Name = "Price")]
    public decimal Price { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Stock quantity cannot be negative.")]
    [Display(Name = "Stock Quantity")]
    public int StockQuantity { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Category must be selected.")]
    [Display(Name = "Category")]
    public int CategoryId { get; set; }

    [Required]
    [StringLength(ImageUrlMaxLength)]
    [Display(Name = "Image URL")]
    [RegularExpression("^[\\w./-]+\\.(?:jpg|jpeg|png|gif|webp|bmp)$", ErrorMessage = "Unsupported or invalid file format.")]
    public string ImageUrl { get; set; } = null!;
}


