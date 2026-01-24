using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechHaven.Data.Models;

namespace TechHaven.Data.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{

    public void Configure(EntityTypeBuilder<Product> builder)
    {

    }
}
