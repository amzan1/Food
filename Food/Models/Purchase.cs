using System;
using System.Collections.Generic;

namespace Food.Models;

public partial class Purchase
{
    public decimal Id { get; set; }

    public decimal? UserId { get; set; }

    public decimal? RecipeId { get; set; }

    public DateTime? BayDate { get; set; }

    public virtual Recipe? Recipe { get; set; }

    public virtual User? User { get; set; }
}
