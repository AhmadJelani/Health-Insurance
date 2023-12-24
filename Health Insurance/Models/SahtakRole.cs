using System;
using System.Collections.Generic;

namespace Health_Insurance.Models;

public partial class SahtakRole
{
    public decimal Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<SahtakUser> SahtakUsers { get; set; } = new List<SahtakUser>();
}
