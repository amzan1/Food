using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Food.Models;

public partial class Recipe
{
    public decimal RecipeId { get; set; }

    public string RecipepName { get; set; } = null!;

    public string RecipeTime { get; set; } = null!;

    public DateTime? RecipeDate { get; set; }

    public string? RecipeStatus { get; set; }

    public decimal? CategoryId { get; set; }

    public decimal? ChefId { get; set; }

    public decimal Price { get; set; }

    public string RecipeIngredients { get; set; } = null!;

    public string RecipePreparation { get; set; } = null!;

    public string? Image1 { get; set; }
    [NotMapped]
    public IFormFile? ImageFile { get; set; }


    public string? Image2 { get; set; }

    public string? Image3 { get; set; }

    public virtual Category? Category { get; set; }

    public virtual User? Chef { get; set; }

    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
}
