using System;
using System.Collections.Generic;

namespace CLExtras2.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public string? ProductDescription { get; set; }

    public string? AlergicType { get; set; }

    public virtual ICollection<Box> Boxes { get; set; } = new List<Box>();
}
