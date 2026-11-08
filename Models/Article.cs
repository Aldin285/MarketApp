using System;
using System.Collections.Generic;

namespace MarketApp.Models;

public partial class Article
{
    public long Id { get; set; }

    public long CategoryId { get; set; }

    public string? Barcode { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public string Unit { get; set; } = "piece";

    public int StockQuantity { get; set; }

    public int MinimumStock { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
