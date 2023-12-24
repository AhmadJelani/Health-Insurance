using System;
using System.Collections.Generic;

namespace Health_Insurance.Models;

public partial class SahtakSubscription
{
    public decimal Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public string? Period { get; set; }

    public virtual ICollection<SahtakBeneficiary> SahtakBeneficiaries { get; set; } = new List<SahtakBeneficiary>();

    public virtual ICollection<SahtakPayment> SahtakPayments { get; set; } = new List<SahtakPayment>();

    public virtual ICollection<UserSubscription> UserSubscriptions { get; set; } = new List<UserSubscription>();
}
