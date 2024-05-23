using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class RecipeUser
{
    public decimal RecipeId { get; set; }

    public string? Email { get; set; }

    public DateTime? PurchaseDate { get; set; }

    public virtual User? EmailNavigation { get; set; }

    public virtual Recipe Recipe { get; set; } = null!;
}
