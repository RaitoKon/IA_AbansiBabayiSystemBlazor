using System;
using System.Collections.Generic;

namespace IA_AbansiBabayiSystemBlazor.Data.Models;

public partial class TroopLeaderRegistration
{
    public int LeaderId { get; set; }

    public string LeaderPosition { get; set; } = null!;

    public string LeaderRole { get; set; } = null!;

    public int? ColeaderTroopNo { get; set; }

    public string LeaderTorNT { get; set; } = null!;

    public string LeaderRegStatus { get; set; } = null!;

    public string LeaderLname { get; set; } = null!;

    public string LeaderFname { get; set; } = null!;

    public string LeaderMinitial { get; set; } = null!;

    public DateTime LeaderBirthdate { get; set; }

    public string LeaderBeneficiary { get; set; } = null!;

    public string LeaderEmail { get; set; } = null!;
}
