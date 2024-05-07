using System;
using System.Collections.Generic;

namespace Food.Models;

public partial class UserLogin
{
    public decimal Id { get; set; }

    public decimal UserId { get; set; }

    public decimal RoleId { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
