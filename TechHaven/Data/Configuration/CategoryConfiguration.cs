using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechHaven.Data.Models;

namespace TechHaven.Data.Configuration;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    private readonly Category[] categories = [

    new Category { Id = 1, Name = "CPU" },

    new Category { Id = 2, Name = "GPU" },

    new Category { Id = 3, Name = "Motherboard" },

    new Category { Id = 4, Name = "RAM" },

    new Category { Id = 5, Name = "Storage" },

    new Category { Id = 6, Name = "Power Supply" },

    new Category { Id = 7, Name = "Cooling" },

    new Category { Id = 8, Name = "Case" },

    new Category {Id = 9, Name = "Peripheral"},

    new Category {Id = 10, Name = "Monitor"}

    ];
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasData(categories);
    }
}
