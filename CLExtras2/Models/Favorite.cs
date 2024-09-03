using System;
using System.Collections.Generic;

namespace CLExtras2.Models;

public partial class Favorite
{
    public int FavoriteId { get; set; }

    public int CustomerId { get; set; }

    public int BusinessId { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
