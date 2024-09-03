using System;
using System.Collections.Generic;

namespace CLExtras2.Models;

public partial class Box
{
    public int BoxId { get; set; }

    public int? BusinessId { get; set; }

    public int? OrderId { get; set; }

    public string? BoxName { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public int? QuantityAvailable { get; set; }
    public decimal? Sale_Price { get; set; }

    public DateTime? DateAdded { get; set; }

    public string? BoxImage { get; set; }

    public string? AlergicType { get; set; }

    public virtual Business? Business { get; set; }

    public virtual Order? Order { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
