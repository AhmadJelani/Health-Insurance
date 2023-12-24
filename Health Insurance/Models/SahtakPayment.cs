using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Health_Insurance.Models;

public partial class SahtakPayment
{
    public decimal Id { get; set; }

    public string? CardNameholder { get; set; }

    public string? Status { get; set; }

    public DateTime? PaymentDate { get; set; }

    public decimal? UserId { get; set; }

    public decimal? BankId { get; set; }
    public Decimal? Amount { get; set; }
    public decimal? SubscriptionId { get; set; }

    public string? CardNumber { get; set; }

    public virtual SahtakBank? Bank { get; set; }

    public virtual SahtakSubscription? Subscription { get; set; }

    public virtual SahtakUser? User { get; set; }
}
