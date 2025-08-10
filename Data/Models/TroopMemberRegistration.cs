using System;
using System.Collections.Generic;

namespace IA_AbansiBabayiSystemBlazor.Data.Models;

public partial class TroopMemberRegistration
{
    public int TroopMemId { get; set; }

    public string? TroopMemRole { get; set; }

    public int? TroopMemScoutNumber { get; set; }

    public int? TroopMemTroopNumber { get; set; }

    public string? TroopMemLname { get; set; }

    public string? TroopMemFname { get; set; }

    public string? TroopMemMinitial { get; set; }

    public DateTime? TroopMemBirthdate { get; set; }

    public string? TroopMemGradeOrYear { get; set; }

    public string? TroopMemRegStatus { get; set; }

    public string? TroopMemBeneficiary { get; set; }

    public string? TroopMemEmail { get; set; }
}
