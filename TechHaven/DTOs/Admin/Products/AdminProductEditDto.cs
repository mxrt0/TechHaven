namespace TechHaven.DTOs.Admin.Products;

public class AdminProductEditDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }

    public int CategoryId { get; set; }
    public string ImageUrl { get; set; } = null!;
    public bool IsActive { get; set; }
}

