using System;
using MarketApp.Models;

namespace MarketApp.DTO.Category;

public class CategoryDto
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }
    public virtual ICollection<Models.Article> Articles { get; set; } = new List<Models.Article>();
}
