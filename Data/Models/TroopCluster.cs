using System;
using System.Collections.Generic;

namespace IA_AbansiBabayiSystemBlazor.Data.Models;

public partial class TroopCluster
{
    public int ClusterId { get; set; }

    public string? ClusterName { get; set; }

    public virtual ICollection<RegisteredTroopMember> RegisteredTroopMembers { get; set; } = new List<RegisteredTroopMember>();
}
