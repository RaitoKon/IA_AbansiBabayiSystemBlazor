using System;
using System.Collections.Generic;

namespace IA_AbansiBabayiSystemBlazor.Data.Models;

public partial class TroopLeaderAccount
{
    public int AccountsId { get; set; }

    public int LeaderId { get; set; }

    public string Id { get; set; } = null!;

    public virtual RegisteredTroopLeader Leader { get; set; } = null!;
}
