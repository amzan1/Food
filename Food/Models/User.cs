using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Food.Models;

public partial class User
{
    public decimal UserId { get; set; }

    public string FName { get; set; } = null!;

    public string LName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? ImagePath { get; set; }
    [NotMapped]
    public IFormFile? ImageFile { get; set; }


    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();

    public virtual ICollection<Testimonial> Testimonials { get; set; } = new List<Testimonial>();

    public virtual ICollection<UserLogin> UserLogins { get; set; } = new List<UserLogin>();

    public virtual ICollection<Visa> Visas { get; set; } = new List<Visa>();
}
