using System;
using System.Collections.Generic;

namespace IA_AbansiBabayiSystemBlazor.Data.Models;

public partial class TroopInformation
{
    public int TroopInfoId { get; set; }
    public int? TroopNo { get; set; }
    public int? TroopDetailsId { get; set; }

    public int? TroopLeaderId { get; set; }

    public int? TroopMemId { get; set; }

    public string? PaymentStatus { get; set; }

    public virtual TroopDetail? TroopDetails { get; set; } = null!;

    public virtual TroopLeader TroopLeader { get; set; } = null!;

    public virtual TroopMember? TroopMem { get; set; } = null!;

}
