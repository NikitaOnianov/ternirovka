using System;
using System.Collections.Generic;

namespace Onianov.Models;

public partial class Producer
{
    public int ProducerId { get; set; }

    public string? ProducerName { get; set; }

    public virtual ICollection<Tovar> Tovars { get; set; } = new List<Tovar>();
}
