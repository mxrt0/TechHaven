using Microsoft.AspNetCore.Mvc;
using TechHaven.DTOs.Order;

namespace TechHaven.Models;

public class OrderDetailsViewModel
{
    public OrderListDto Order { get; set; } = null!;
}
