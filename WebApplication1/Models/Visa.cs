using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Visa
{
    public string? CardNumber { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public string? Cvc { get; set; }

    public decimal? Balance { get; set; }
}
