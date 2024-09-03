using System;
using System.Collections.Generic;

namespace CLExtras2.Models;

public partial class Message
{
    public int MessageId { get; set; }

    public string? MessageName { get; set; }

    public string? MessageText { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
