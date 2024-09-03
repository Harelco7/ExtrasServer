using System;
using System.Collections.Generic;

namespace CLExtras2.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }

    public string? UserType { get; set; }

    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }

    public int? MessageId { get; set; }

    public virtual ICollection<Business> Businesses { get; set; } = new List<Business>();

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual Message? Message { get; set; }
}
