using System;
using System.Collections.Generic;

namespace CLExtras2.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public int? UserId { get; set; }

    public int? FavoriteId { get; set; }

    public string? CustomerName { get; set; }

    public string Gender { get; set; } = null!;

    public int Age { get; set; }

    public virtual Favorite? Favorite { get; set; }

    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual User? User { get; set; }
}
