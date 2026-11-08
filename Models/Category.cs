using System;
using System.Collections.Generic;

namespace MarketApp.Models;

public partial class Category
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();
}
