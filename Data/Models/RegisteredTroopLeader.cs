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

    public string? LeaderMInitial { get; set; }

    public DateOnly? LeaderBirthdate { get; set; }

    public string? LeaderBeneficiary { get; set; }

    public string? LeaderEmail { get; set; }
}
