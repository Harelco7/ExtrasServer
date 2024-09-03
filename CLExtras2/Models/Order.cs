using System;
using System.Collections.Generic;

namespace CLExtras2.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? BoxID { get; set; }


    public int? CustomerId { get; set; }
    public int? BusinessID { get; set; }

    public string boxDescription { get; set; }

    public DateTime? OrderDate { get; set; }

    public int? QuantityOrdered { get; set; }

    public decimal? TotalPrice { get; set; }

    public string? OrderStatus { get; set; }

    public int? OrderRaiting { get; set; }

    public virtual ICollection<Box> Boxes { get; set; } = new List<Box>();

    public virtual Customer? Customer { get; set; }
}
