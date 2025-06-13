using System;
using System.Collections.Generic;

namespace IA_AbansiBabayiSystemBlazor.Data.Models;

public partial class Login
{
    public int UserId { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public int AuthRoleId { get; set; }

    public virtual AuthorizationRole? AuthRoleNavigation { get; set; }
}
