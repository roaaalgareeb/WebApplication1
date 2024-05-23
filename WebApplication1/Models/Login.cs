using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Login
{
    public decimal LoginId { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public decimal? RoleId { get; set; }

    public decimal? UserId { get; set; }

    public string? Imagepath { get; set; }

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();

    public virtual Role? Role { get; set; }
}
