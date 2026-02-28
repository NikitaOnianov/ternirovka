using System;
using System.Collections.Generic;

namespace Onianov.Models;

public partial class Delivere
{
    public int DelivereId { get; set; }

    public string? DelivereName { get; set; }

    public virtual ICollection<Tovar> Tovars { get; set; } = new List<Tovar>();
}
