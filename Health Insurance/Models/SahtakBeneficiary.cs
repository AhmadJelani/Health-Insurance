using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Health_Insurance.Models;

public partial class SahtakBeneficiary
{
    public decimal Id { get; set; }

    public string? Relationship { get; set; }
    [NotMapped]
    public IFormFile PDFFile { get; set; }
    public string? ProofOfRelationship { get; set; }

    public string? Status { get; set; }

    public decimal? SubscriptionId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public decimal? UserId { get; set; }

    public virtual SahtakSubscription? Subscription { get; set; }

    public virtual SahtakUser? User { get; set; }
}
