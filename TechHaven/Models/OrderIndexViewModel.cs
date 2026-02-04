using Microsoft.AspNetCore.Mvc;
using TechHaven.DTOs.Order;

namespace TechHaven.Models;

public class OrderIndexViewModel
{
    public IEnumerable<OrderListDto> Orders { get; set; } = new List<OrderListDto>();
}
