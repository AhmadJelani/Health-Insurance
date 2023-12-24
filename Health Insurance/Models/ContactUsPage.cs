using System;
using System.Collections.Generic;

namespace Health_Insurance.Models;

public partial class ContactUsPage
{
    public decimal Id { get; set; }

    public string? Title { get; set; }

    public string? Header { get; set; }

    public string? Paragraph { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Location { get; set; }

    public string? LocationDesc { get; set; }

    public string? WorkingDays { get; set; }

    public string? WorkingDaysDesc { get; set; }
}
