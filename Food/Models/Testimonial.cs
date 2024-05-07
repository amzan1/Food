using System;
using System.Collections.Generic;

namespace Food.Models;

public partial class Testimonial
{
    public decimal TestimonialId { get; set; }

    public decimal? UserId { get; set; }

    public string? TestimonialText { get; set; }

    public string? TestimonialStatus { get; set; }

    public virtual User? User { get; set; }
}
