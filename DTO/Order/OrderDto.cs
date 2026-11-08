using System;
using System.Collections.Generic;
using MarketApp.Models;

namespace MarketApp.DTO.Order;

public class OrderDto
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public string Status { get; set; } = null!;

    public DateTime OrderedAt { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual MarketApp.Models.User User { get; set; } = null!;

}