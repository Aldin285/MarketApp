using System;
using MarketApp.Models;

namespace MarketApp.DTO.Order;

public class OrderItemDto
{
    public long Id { get; set; }

    public long OrderId { get; set; }

    public long ArticleId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public virtual Models.Article Article { get; set; } = null!;
    public virtual Models.Order Order { get; set; } = null!;
}
