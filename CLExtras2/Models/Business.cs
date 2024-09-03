using System;
using System.Collections.Generic;

namespace CLExtras2.Models;

public partial class Business
{
    public int BusinessId { get; set; }

    public int? UserId { get; set; }

    public string? BusinessName { get; set; }

    public string? BusinessType { get; set; }

    public string? ContactInfo { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    public string? BusinessPhoto { get; set; }

    public string? BusinessLogo { get; set; }

    public string? OpeningHours { get; set; }

    public string? DailySalesHour { get; set; }

    public string? Address { get; set; }
     
    public string? Business_description { get; set; }

    public virtual ICollection<Box> Boxes { get; set; } = new List<Box>();

    public virtual User? User { get; set; }
}
