using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechHaven.Data.Models;

namespace TechHaven.Data.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    private readonly Product[] products = [

    new Product
    {
        Id = 1,
        Name = "Intel Core i5-12400F",
        CategoryId = 1,
        Description = "6-core mid-range CPU with great single-thread performance.",
        SpecsJson = "{\"Cores\":6,\"Threads\":12,\"BaseClock\":\"2.5GHz\",\"BoostClock\":\"4.4GHz\",\"Socket\":\"LGA1700\",\"Manufacturer\":\"Intel\"}",
        Price = 229.99M,
        StockQuantity = 20,
        ImageUrl = "/images/products/intel_i5_12400f.jpg"
    },
    new Product
    {
        Id = 2,
        Name = "AMD Ryzen 5 5600X",
        CategoryId = 1,
        Description = "Affordable 6-core Ryzen CPU with excellent gaming performance.",
        SpecsJson = "{\"Cores\":6,\"Threads\":12,\"BaseClock\":\"3.7GHz\",\"BoostClock\":\"4.6GHz\",\"Socket\":\"AM4\",\"Manufacturer\":\"AMD\"}",
        Price = 199.99M,
        StockQuantity = 15,
        ImageUrl = "/images/products/amd_ryzen_5600x.jpg"
    },
    new Product
    {
        Id = 3,
        Name = "Intel Core i7-12700K",
        CategoryId = 1,
        Description = "12-core high-end CPU suitable for gaming and content creation.",
        SpecsJson = "{\"Cores\":12,\"Threads\":20,\"BaseClock\":\"3.6GHz\",\"BoostClock\":\"5.0GHz\",\"Socket\":\"LGA1700\",\"Manufacturer\":\"Intel\"}",
        Price = 699.99M,
        StockQuantity = 10,
        ImageUrl = "/images/products/intel_i7_12700k.jpg"
    },

    new Product
    {
        Id = 4,
        Name = "ASUS TUF Gaming GeForce RTX 3060",
        CategoryId = 2,
        Description = "Great value GPU for 1080p & 1440p gaming.",
        SpecsJson = "{\"VRAM\":\"12GB\",\"BoostClock\":\"1.78GHz\",\"PCIe\":\"4.0\",\"Brand\":\"Nvidia\",\"Manufacturer\":\"ASUS\"}",
        Price = 529.99M,
        StockQuantity = 10,
        ImageUrl = "/images/products/asus_rtx3060.jpg"
    },
    new Product
    {
        Id = 5,
        Name = "MSI GeForce RTX 4080 SUPER",
        CategoryId = 2,
        Description = "High-end Nvidia GPU for 4K gaming and heavy workloads.",
        SpecsJson = "{\"VRAM\":\"16GB\",\"BoostClock\":\"2.55GHz\",\"PCIe\":\"4.0\",\"Brand\":\"Nvidia\",\"Manufacturer\":\"MSI\"}",
        Price = 2699.99M,
        StockQuantity = 5,
        ImageUrl = "/images/products/msi_rtx4080.jpg"
    },
    new Product
    {
        Id = 6,
        Name = "Gigabyte Radeon RX 6700 XT",
        CategoryId = 2,
        Description = "Powerful AMD GPU for 1440p gaming with ray tracing.",
        SpecsJson = "{\"VRAM\":\"12GB\",\"BoostClock\":\"2.4GHz\",\"PCIe\":\"4.0\",\"Manufacturer\":\"Gigabyte\",\"Brand\":\"AMD\"}",
        Price = 679.99M,
        StockQuantity = 7,
        ImageUrl = "/images/products/gigabyte_6700xt.jpg"
    },

    new Product
    {
        Id = 7,
        Name = "ASRock B650M-HDV/M.2",
        CategoryId = 3,
        Description = "Low-budget AMD AM5 motherboard with very good price-to-performance ratio, capable of handling even more powerful CPUs.",
        SpecsJson = "{\"Socket\":\"AM5\",\"FormFactor\":\"Micro-ATX\",\"Chipset\":\"B650\"}",
        Price = 169.99M,
        StockQuantity = 12,
        ImageUrl = "/images/products/asrock_b650m.jpg"
    },
    new Product
    {
        Id = 8,
        Name = "MSI Z690-A Pro",
        CategoryId = 3,
        Description = "Intel LGA1700 motherboard for 12th Gen CPUs with PCIe 5.0 support.",
        SpecsJson = "{\"Socket\":\"LGA1700\",\"FormFactor\":\"ATX\",\"Chipset\":\"Z690\"}",
        Price = 259.99M,
        StockQuantity = 8,
        ImageUrl = "/images/products/msi_z690a.jpg"
    },
    new Product
    {
        Id = 9,
        Name = "Gigabyte B660M DS3H",
        CategoryId = 3,
        Description = "Affordable LGA1700 motherboard for mid-range builds.",
        SpecsJson = "{\"Socket\":\"LGA1700\",\"FormFactor\":\"Micro-ATX\",\"Chipset\":\"B660\"}",
        Price = 129.99M,
        StockQuantity = 15,
        ImageUrl = "/images/products/gigabyte_b660m.jpg"
    },

    new Product
    {
        Id = 10,
        Name = "Corsair Vengeance LPX 16GB",
        CategoryId = 4,
        Description = "High-speed DDR4 memory kit, 3200MHz.",
        SpecsJson = "{\"Capacity\":\"16GB\",\"Type\":\"DDR4\",\"Speed\":\"3200MHz\"}",
        Price = 69.99M,
        StockQuantity = 25,
        ImageUrl = "/images/products/corsair_16gb.jpg"
    },
    new Product
    {
        Id = 11,
        Name = "G.Skill Trident Z RGB 32GB",
        CategoryId = 4,
        Description = "Premium DDR4 RAM with RGB lighting, 3600MHz.",
        SpecsJson = "{\"Capacity\":\"32GB\",\"Type\":\"DDR4\",\"Speed\":\"3600MHz\"}",
        Price = 209.99M,
        StockQuantity = 18,
        ImageUrl = "/images/products/gskill_32gb.jpg"
    },
    new Product
    {
        Id = 12,
        Name = "Kingston Fury Beast 16GB",
        CategoryId = 4,
        Description = "Reliable DDR4 memory for gaming and multitasking.",
        SpecsJson = "{\"Capacity\":\"16GB\",\"Type\":\"DDR4\",\"Speed\":\"3200MHz\"}",
        Price = 109.99M,
        StockQuantity = 30,
        ImageUrl = "/images/products/kingston_16gb.jpg"
    },

    new Product
    {
        Id = 13,
        Name = "Samsung 970 EVO Plus 250GB",
        CategoryId = 5,
        Description = "High-speed NVMe SSD for gaming and productivity.",
        SpecsJson = "{\"Capacity\":\"250GB\",\"FormFactor\":\"M.2\",\"Interface\":\"NVMe\"}",
        Price = 119.99M,
        StockQuantity = 18,
        ImageUrl = "/images/products/samsung_970evo.jpg"
    },
    new Product
    {
        Id = 14,
        Name = "Western Digital Blue 2TB HDD",
        CategoryId = 5,
        Description = "Reliable 2TB HDD for mass storage.",
        SpecsJson = "{\"Capacity\":\"2TB\",\"FormFactor\":\"3.5in\",\"Interface\":\"SATA\"}",
        Price = 59.99M,
        StockQuantity = 25,
        ImageUrl = "/images/products/wd_2tb.jpg"
    },
    new Product
    {
        Id = 15,
        Name = "Crucial P5 Plus 1TB",
        CategoryId = 5,
        Description = "High-performance NVMe SSD with PCIe 4.0 support.",
        SpecsJson = "{\"Capacity\":\"1TB\",\"FormFactor\":\"M.2\",\"Interface\":\"NVMe\"}",
        Price = 129.99M,
        StockQuantity = 20,
        ImageUrl = "/images/products/crucial_p5plus.jpg"
    },

    new Product
    {
        Id = 16,
        Name = "Corsair RM750x",
        CategoryId = 6,
        Description = "750W 80+ Gold modular PSU with quiet operation.",
        SpecsJson = "{\"Wattage\":\"750W\",\"Efficiency\":\"80+ Gold\",\"Modular\":\"Yes\"}",
        Price = 169.99M,
        StockQuantity = 15,
        ImageUrl = "/images/products/corsair_rm750x.jpg"
    },
    new Product
    {
        Id = 17,
        Name = "EVGA SuperNOVA 650 G5",
        CategoryId = 6,
        Description = "Reliable 650W 80+ Gold fully modular power supply.",
        SpecsJson = "{\"Wattage\":\"650W\",\"Efficiency\":\"80+ Gold\",\"Modular\":\"No\"}",
        Price = 99.99M,
        StockQuantity = 20,
        ImageUrl = "/images/products/evga_650g5.jpg"
    },
    new Product
    {
        Id = 18,
        Name = "Seasonic Focus GX-850",
        CategoryId = 6,
        Description = "High-quality 850W PSU for high-end systems.",
        SpecsJson = "{\"Wattage\":\"850W\",\"Efficiency\":\"80+ Gold\",\"Modular\":\"Yes\"}",
        Price = 239.99M,
        StockQuantity = 10,
        ImageUrl = "/images/products/seasonic_850w.jpg"
    },

    new Product
    {
        Id = 19,
        Name = "DeepCool AK400",
        CategoryId = 7,
        Description = "Low-budget single-tower air cooler for less demanding CPUs.",
        SpecsJson = "{\"Type\":\"Air Cooler\",\"Fans\":1}",
        Price = 49.99M,
        StockQuantity = 11,
        ImageUrl = "/images/products/deepcool_ak400.jpg"
    },
    new Product
    {
        Id = 20,
        Name = "Corsair iCUE H100i RGB",
        CategoryId = 7,
        Description = "All-in-one liquid cooler with RGB lighting.",
        SpecsJson = "{\"Type\":\"Liquid Cooler\",\"RadiatorSize\":\"240mm\"}",
        Price = 169.99M,
        StockQuantity = 10,
        ImageUrl = "/images/products/corsair_h100i.jpg"
    },
    new Product
    {
        Id = 21,
        Name = "be quiet! Dark Rock Pro 4",
        CategoryId = 7,
        Description = "Quiet and efficient air CPU cooler.",
        SpecsJson = "{\"Type\":\"Air Cooler\",\"Fans\":2}",
        Price = 89.99M,
        StockQuantity = 12,
        ImageUrl = "/images/products/darkrock_pro4.jpg"
    },

    new Product
    {
        Id = 22,
        Name = "DeepCool CH360",
        CategoryId = 8,
        Description = "Mini-tower case with RGB fans, clean mesh design and exceptional airflow.",
        SpecsJson = "{\"FormFactor\":\"Mini Tower\",\"Color\":\"Black\"}",
        Price = 150.99M,
        StockQuantity = 20,
        ImageUrl = "/images/products/deepcool_ch360.jpg"
    },
    new Product
    {
        Id = 23,
        Name = "Fractal Design Meshify C White",
        CategoryId = 8,
        Description = "High airflow mid-tower case with mesh front panel.",
        SpecsJson = "{\"FormFactor\":\"Mid Tower\",\"Color\":\"White\"}",
        Price = 120.99M,
        StockQuantity = 15,
        ImageUrl = "/images/products/meshify_c.jpg"
    },
    new Product
    {
        Id = 24,
        Name = "Corsair 4000D Airflow",
        CategoryId = 8,
        Description = "Compact mid-tower case optimized for airflow.",
        SpecsJson = "{\"FormFactor\":\"Mid Tower\",\"Color\":\"Black\"}",
        Price = 99.99M,
        StockQuantity = 18,
        ImageUrl = "/images/products/corsair_4000d.jpg"
    },

    new Product
    {
        Id = 25,
        Name = "Logitech G502 HERO",
        CategoryId = 9,
        Description = "Wired gaming mouse with adjustable DPI and 11 buttons.",
        SpecsJson = "{\"Type\":\"Mouse\",\"DPI\":\"16000\",\"Buttons\":11,\"Connectivity\":\"Wired\",\"RGB\":\"No\"}",
        Price = 109.99M,
        StockQuantity = 30,
        ImageUrl = "/images/products/logitech_g502.jpg"
    },
    new Product
    {
        Id = 26,
        Name = "Corsair K70 RGB MK.2",
        CategoryId = 9,
        Description = "Mechanical gaming keyboard with Cherry MX switches.",
        SpecsJson = "{\"Type\":\"Keyboard\",\"Switch\":\"Cherry MX Red\",\"Keys\":104,\"Connectivity\":\"Wired\",\"RGB\":\"Yes\"}",
        Price = 129.99M,
        StockQuantity = 25,
        ImageUrl = "/images/products/corsair_k70.jpg"
    },
    new Product
    {
        Id = 27,
        Name = "Razer DeathAdder V2",
        CategoryId = 9,
        Description = "Ergonomic gaming mouse with high-precision optical sensor.",
        SpecsJson = "{\"Type\":\"Mouse\",\"DPI\":\"20000\",\"Buttons\":8,\"Connectivity\":\"Wired\",\"RGB\":\"No\"}",
        Price = 69.99M,
        StockQuantity = 28,
        ImageUrl = "/images/products/razer_deathadder_v2.jpg"
    },

    new Product
    {
        Id = 28,
        Name = "Dell UltraSharp U2720Q",
        CategoryId = 10,
        Description = "27-inch 4K IPS monitor with excellent color accuracy.",
        SpecsJson = "{\"Size\":\"27in\",\"Resolution\":\"3840x2160\",\"Panel\":\"IPS\",\"RefreshRate\":\"60Hz\"}",
        Price = 499.99M,
        StockQuantity = 10,
        ImageUrl = "/images/products/dell_u2720q.jpg"
    },
    new Product
    {
        Id = 29,
        Name = "ASUS TUF Gaming VG27AQ",
        CategoryId = 10,
        Description = "27-inch QHD gaming monitor with 165Hz refresh rate.",
        SpecsJson = "{\"Size\":\"27in\",\"Resolution\":\"2560x1440\",\"Panel\":\"IPS\",\"RefreshRate\":\"165Hz\"}",
        Price = 399.99M,
        StockQuantity = 15,
        ImageUrl = "/images/products/asus_vg27aq.jpg"
    },
    new Product
    {
        Id = 30,
        Name = "Samsung Odyssey G3",
        CategoryId = 10,
        Description = "24-inch FHD gaming monitor with 180Hz refresh rate and a VA panel.",
        SpecsJson = "{\"Size\":\"24in\",\"Resolution\":\"1920x1080\",\"Panel\":\"VA\",\"RefreshRate\":\"180Hz\"}",
        Price = 249.99M,
        StockQuantity = 12,
        ImageUrl = "/images/products/odyssey_g3.jpg"
    },
    new Product
    {
        Id = 31,
        Name = "Razer Hunstman V2 TKL",
        CategoryId = 9,
        Description = "Tenkeyless mechanical gaming keyboard with Razer linear switches.",
        SpecsJson = "{\"Type\":\"Keyboard\",\"Switch\":\"Razer\",\"Keys\":88,\"Connectivity\":\"Wired\",\"RGB\":\"Yes\"}",
        Price = 169.99M,
        StockQuantity = 25,
        ImageUrl = "/images/products/razer_huntsman.jpg"
    }
];


    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasData(products);
    }
}
