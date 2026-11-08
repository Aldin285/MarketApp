using MarketApp.DTO.User;
using MarketApp.Models;

namespace WebApplication1.DTO.User
{
    public class UserDto
    {
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Type { get; set; } = UserTypes.employee.ToString()!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    }
}
