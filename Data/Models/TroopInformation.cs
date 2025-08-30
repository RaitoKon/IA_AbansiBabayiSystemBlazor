using System;
using System.Collections.Generic;

namespace IA_AbansiBabayiSystemBlazor.Data.Models;

public partial class TroopInformation
{
    public int TroopInfoId { get; set; }

    public string TroopName { get; set; } = null!;

    public int TroopAgeLevel { get; set; }

    public string TroopAddress { get; set; } = null!;

    public string TroopSponsoringGroup { get; set; } = null!;

    public int TroopTroopTelNo { get; set; }

    public string TroopDistrictCommittee { get; set; } = null!;

    public string TroopMailingAddress { get; set; } = null!;

    public string TroopBarangayCommittee { get; set; } = null!;

    public string TroopType { get; set; } = null!;

    public string TroopStatus { get; set; } = null!;

    public int LeaderId { get; set; }

    public int TroopMemId { get; set; }

    public virtual RegisteredTroopLeader Leader { get; set; } = null!;

    public virtual RegisteredTroopMember TroopMem { get; set; } = null!;
}
