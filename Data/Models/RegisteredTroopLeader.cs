using System;
using System.Collections.Generic;

namespace IA_AbansiBabayiSystemBlazor.Data.Models;

public partial class RegisteredTroopLeader
{
    public int LeaderId { get; set; }

    public string? LeaderPosition { get; set; }

    public string? LeaderRole { get; set; }

    public string? LeaderTorNT { get; set; }

    public string? LeaderRegStatus { get; set; }

    public string? LeaderLname { get; set; }

    public string? LeaderFname { get; set; }

    public string? LeaderMinitial { get; set; }

    public DateTime? LeaderBirthdate { get; set; }

    public string? LeaderBeneficiary { get; set; }

    public string? LeaderEmail { get; set; }

    public int? ColeaderTroopNo { get; set; }

    public virtual ICollection<TroopInformation> TroopInformations { get; set; } = new List<TroopInformation>();

    public virtual ICollection<TroopLeaderAccount> TroopLeaderAccounts { get; set; } = new List<TroopLeaderAccount>();
}
