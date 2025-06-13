using System;
using System.Collections.Generic;

namespace IA_AbansiBabayiSystemBlazor.Data.Models;

public partial class AuthorizationRole
{
    public int AuthRoleId { get; set; }

    public string? AuthRoleName { get; set; }

    public ICollection<Login> Logins { get; set; }
}
