using System;
using System.Collections.Generic;

namespace Onianov.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public DateOnly OrderDateCreate { get; set; }

    public DateOnly OrderDateDelivery { get; set; }

    public int OrderAddress { get; set; }

    public int OrderUser { get; set; }

    public int OrderCode { get; set; }

    public int OrderStatus { get; set; }

    public virtual Address OrderAddressNavigation { get; set; } = null!;

    public virtual OrderStatus OrderStatusNavigation { get; set; } = null!;

    public virtual ICollection<OrderTovar> OrderTovars { get; set; } = new List<OrderTovar>();

    public virtual User OrderUserNavigation { get; set; } = null!;
}
