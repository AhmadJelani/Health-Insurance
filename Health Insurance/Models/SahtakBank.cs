using System;
using System.Collections.Generic;

namespace Health_Insurance.Models;

public partial class SahtakBank
{
    public decimal Id { get; set; }

    public string? AccountName { get; set; }

    public decimal? Amount { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<SahtakPayment> SahtakPayments { get; set; } = new List<SahtakPayment>();
}
