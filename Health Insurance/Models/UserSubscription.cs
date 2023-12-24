using System;
using System.Collections.Generic;

namespace Health_Insurance.Models;

public partial class UserSubscription
{
    public decimal Id { get; set; }

    public decimal? UserId { get; set; }

    public decimal? SubscriptionId { get; set; }

    public DateTime? DateFrom { get; set; }

    public DateTime? DateTo { get; set; }

    public DateTime? PaymentDate { get; set; }

    public virtual SahtakSubscription? Subscription { get; set; }

    public virtual SahtakUser? User { get; set; }
}
