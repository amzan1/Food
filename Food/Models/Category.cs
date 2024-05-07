using System;
using System.Collections.Generic;

namespace Food.Models;

public partial class Category
{
    public decimal Id { get; set; }

    public string? CategoryName { get; set; }

    public string? ImagePath { get; set; }

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}
