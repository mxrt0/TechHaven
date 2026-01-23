using Microsoft.AspNetCore.Identity;

namespace TechHaven.Data.Models;

public class ApplicationUser : IdentityUser
{
    public string? DisplayName { get; set; }
    public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
}
