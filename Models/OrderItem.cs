using System;
using System.Collections.Generic;

namespace MarketApp.Models;

public partial class OrderItem
{
    public long Id { get; set; }

    public long OrderId { get; set; }

    public long ArticleId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public virtual Article Article { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
