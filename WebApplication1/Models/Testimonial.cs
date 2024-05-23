using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Testimonial
{
    public decimal TestimonialId { get; set; }

    public string? Email { get; set; }

    public decimal? Rating { get; set; }

    public string? Testi { get; set; }

    public decimal? UserId { get; set; }

    public virtual User? EmailNavigation { get; set; }

    public virtual User? User { get; set; }
}
