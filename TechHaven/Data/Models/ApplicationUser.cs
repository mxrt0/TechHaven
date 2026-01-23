using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static TechHaven.Common.EntityValidation.ApplicationUser;
namespace TechHaven.Data.Models;

public class ApplicationUser : IdentityUser
{
    [MaxLength(DisplayNameMaxLength)]
    public string? DisplayName { get; set; }
    public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
}
