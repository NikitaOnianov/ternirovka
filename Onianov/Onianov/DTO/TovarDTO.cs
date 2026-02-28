using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onianov.Models;

public partial class TovarDTO
{
    public string TovarId { get; set; } = null!;

    public string? TovarName { get; set; }

    public decimal? TovarPrice { get; set; }

    public string TovarDelivary { get; set; }

    public string TovarProducer { get; set; }

    public string TovarType { get; set; }

    public int? TovarSkidka { get; set; }

    public int? TovarCount { get; set; }

    public string? TovarDesription { get; set; }

    public Bitmap TovarPhoto { get; set; }
}
