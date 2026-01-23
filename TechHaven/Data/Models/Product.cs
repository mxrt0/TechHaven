using System.ComponentModel.DataAnnotations;
using TechHaven.Data.Enums;
namespace TechHaven.Data.Models;

public class Product
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    public Category Category { get; set; }

    [Required]
    public string Description { get; set; } = null!;

    [Required]
    public string SpecsJson { get; set; } = null!;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }

    [Required]
    public string ImageUrl { get; set; } = null!;

}
