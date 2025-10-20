using System;
using System.Collections.Generic;

namespace IA_AbansiBabayiSystemBlazor.Data.Models;

public partial class AccountStatus
{
    public int AccountStatusId { get; set; }

    public string? Status { get; set; }

    public string? AccountStatusDescription { get; set; }

    public virtual ICollection<ApplicationUser> AspNetUsers { get; set; } = new List<ApplicationUser>();
}
