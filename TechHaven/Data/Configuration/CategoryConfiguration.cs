using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechHaven.Data.Models;

namespace TechHaven.Data.Configuration;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    private readonly Category[] categories = [

    new Category { Id = 1, Name = "CPU", ImageUrl = "/images/categories/cpu.jpg" },

    new Category { Id = 2, Name = "GPU", ImageUrl = "/images/categories/gpu.jpg" },

    new Category { Id = 3, Name = "Motherboard", ImageUrl = "/images/categories/motherboard.jpg" },

    new Category { Id = 4, Name = "RAM", ImageUrl = "/images/categories/ram.jpg" },

    new Category { Id = 5, Name = "Storage", ImageUrl = "/images/categories/storage.jpg" },

    new Category { Id = 6, Name = "Power Supply", ImageUrl = "/images/categories/psu.jpg"  },

    new Category { Id = 7, Name = "Cooling", ImageUrl = "/images/categories/cooling.jpg"  },

    new Category { Id = 8, Name = "Case", ImageUrl = "/images/categories/case.jpg"  },

    new Category {Id = 9, Name = "Peripheral", ImageUrl = "/images/categories/peripheral.jpg" },

    new Category {Id = 10, Name = "Monitor", ImageUrl = "/images/categories/monitor.jpg" }

    ];
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasData(categories);
    }
}
