using System;
using MarketApp.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MarketApp.DTO.Article;

public class ArticleDto
{
    public long Id { get; set; }

    public long CategoryId { get; set; }

    public string? Barcode { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public string Unit { get; set; } = null!;

    public int StockQuantity { get; set; }

    public int MinimumStock { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Models.Category Category { get; set; } = null!;

    public virtual ICollection<Models.OrderItem> OrderItems { get; set; } = new List<Models.OrderItem>();

}
