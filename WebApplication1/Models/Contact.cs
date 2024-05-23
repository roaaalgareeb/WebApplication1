using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Contact
{
    public decimal ContactId { get; set; }

    public string? Email { get; set; }

    public string? Message { get; set; }

    public string? UserEmail { get; set; }

    public virtual User? UserEmailNavigation { get; set; }
}
