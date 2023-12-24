using System;
using System.Collections.Generic;

namespace Health_Insurance.Models;

public partial class SahtakUser
{
    public decimal Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public string? Password { get; set; }

    public DateTime? JoinDate { get; set; }

    public decimal? RoleId { get; set; }

    public virtual SahtakRole? Role { get; set; }

    public virtual ICollection<SahtakPayment> SahtakPayments { get; set; } = new List<SahtakPayment>();

    public virtual ICollection<SahtakTestimonial> SahtakTestimonials { get; set; } = new List<SahtakTestimonial>();

    public virtual ICollection<UserSubscription> UserSubscriptions { get; set; } = new List<UserSubscription>();

    public virtual ICollection<SahtakBeneficiary> SahtakBeneficiaries { get; set; } = new List<SahtakBeneficiary>();

}
