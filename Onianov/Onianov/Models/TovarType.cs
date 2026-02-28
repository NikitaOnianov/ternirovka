using System;
using System.Collections.Generic;

namespace Onianov.Models;

public partial class TovarType
{
    public int TovarId { get; set; }

    public string? TovarName { get; set; }

    public virtual ICollection<Tovar> Tovars { get; set; } = new List<Tovar>();
}
