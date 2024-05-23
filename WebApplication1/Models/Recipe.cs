using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

public partial class Recipe
{
    public decimal RecipeId { get; set; }

    public string? RecipeName { get; set; }

    public string? Ingredients { get; set; }

    public string? Instructions { get; set; }

    public decimal? CategoryId { get; set; }

    public string? Email { get; set; }

    public string? Isaccepted { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? ImagePath1 { get; set; }
    [NotMapped]
    public virtual IFormFile ImageFile { get; set; }

    public decimal? Price { get; set; }

    public decimal UserId { get; set; }

    public virtual Category? Category { get; set; }

    public virtual RecipeUser? RecipeUser { get; set; }

    public virtual Login User { get; set; } = null!;
}
