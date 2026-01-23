using System.ComponentModel.DataAnnotations;

namespace TechHaven.Data.Models;

public class Category
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;
}
