using System.ComponentModel.DataAnnotations;
using static TechHaven.Common.EntityValidation.Category;
namespace TechHaven.Data.Models;

public class Category
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(NameMaxLength)]
    public string Name { get; set; } = null!;

    [Required]
    [MaxLength(ImageUrlMaxLength)]
    public string ImageUrl { get; set; } = null!;
}
