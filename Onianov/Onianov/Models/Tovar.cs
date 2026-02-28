using System;
using System.Collections.Generic;

namespace Onianov.Models;

public partial class Tovar
{
    public string TovarId { get; set; } = null!;

    public string? TovarName { get; set; }

    public decimal? TovarPrice { get; set; }

    public int TovarDelivary { get; set; }

    public int TovarProducer { get; set; }

    public int TovarType { get; set; }

    public int? TovarSkidka { get; set; }

    public int? TovarCount { get; set; }

    public string? TovarDesription { get; set; }

    public string? TovarPhoto { get; set; }

    public virtual ICollection<OrderTovar> OrderTovars { get; set; } = new List<OrderTovar>();

    public virtual Delivere TovarDelivaryNavigation { get; set; } = null!;

    public virtual Producer TovarProducerNavigation { get; set; } = null!;

    public virtual TovarType TovarTypeNavigation { get; set; } = null!;
}
