using System;
using System.Collections.Generic;

namespace Food.Models;

public partial class Subscribe
{
    public decimal SubscriberId { get; set; }

    public string Email { get; set; } = null!;

    public DateTime? SubscriptionDate { get; set; }
}
