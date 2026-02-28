using System;
using System.Collections.Generic;

namespace Onianov.Models;

public partial class OrderTovar
{
    public int OrderTovarId { get; set; }

    public string OrderTovarTovar { get; set; } = null!;

    public int OrderTovarOrder { get; set; }

    public virtual Order OrderTovarOrderNavigation { get; set; } = null!;

    public virtual Tovar OrderTovarTovarNavigation { get; set; } = null!;
}
