using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Category
{
    public decimal CategoryId { get; set; }

    public string? CategoryName { get; set; }

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}
