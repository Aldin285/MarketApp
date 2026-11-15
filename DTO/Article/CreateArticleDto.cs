using System;
using MarketApp.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MarketApp.DTO.Article;

public class CreateArticleDto
{
    public long CategoryId { get; set; } = 1;

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; } = 1.0m;

    public Units Unit { get; set; } = Units.Piece;

    public int StockQuantity { get; set; } = 1;

    public int MinimumStock { get; set; } = 1;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

}
