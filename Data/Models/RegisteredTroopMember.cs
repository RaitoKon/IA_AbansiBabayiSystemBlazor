using System;
using System.Collections.Generic;

namespace IA_AbansiBabayiSystemBlazor.Data.Models;

public partial class RegisteredTroopMember
{
    public int TroopMemId { get; set; }

    public string? TroopMemRole { get; set; }

    public int? TroopMemScoutNumber { get; set; }

    public string? TroopMemLname { get; set; }

    public string? TroopMemFname { get; set; }

    public string? TroopMemMinitial { get; set; }

    public DateOnly? TroopMemBirthdate { get; set; }

    public string? TroopMemGradeOrYear { get; set; }

    public string? TroopMemRegStatus { get; set; }

    public string? TroopMemBeneficiary { get; set; }

    public string? TroopMemEmail { get; set; }

    public int? TroopMemTroopNumber { get; set; }
}
