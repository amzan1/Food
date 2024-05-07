using System;
using System.Collections.Generic;

namespace Food.Models;

public partial class Visa
{
    public decimal VisaId { get; set; }

    public decimal? UserId { get; set; }

    public string CardNumber { get; set; } = null!;

    public string CardHolderName { get; set; } = null!;

    public DateTime ExpirationDate { get; set; }

    public string Cvv { get; set; } = null!;

    public virtual User? User { get; set; }
}
