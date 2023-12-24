using System;
using System.Collections.Generic;

namespace Health_Insurance.Models;

public partial class SahtakTestimonial
{
    public decimal Id { get; set; }

    public string? Text { get; set; }

    public string? Status { get; set; }

    public DateTime? SubmitDate { get; set; }

    public decimal? UserId { get; set; }

    public virtual SahtakUser? User { get; set; }
}
