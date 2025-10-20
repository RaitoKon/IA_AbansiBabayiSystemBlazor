using System;
using System.Collections.Generic;

namespace IA_AbansiBabayiSystemBlazor.Data.Models;

public partial class TroopMember
{
    public int TroopMemId { get; set; }

    public string UserRole { get; set; } = null!;

    public int? TroopMemScoutNumber { get; set; }

    public int? TroopMemScoutLevelId { get; set; }

    public string? TroopMemLname { get; set; }

    public string? TroopMemFname { get; set; }

    public string? TroopMemMname { get; set; }

    public DateTime? TroopMemBirthdate { get; set; }

    public string? TroopMemGradeOrYear { get; set; }

    public string? TroopMemRegStatus { get; set; }

    public string? TroopMemBeneficiary { get; set; }

    public string? TroopMemEmail { get; set; }

    public string? TroopMemRegisteredEmail { get; set; }

    public int? TroopMemTroopNo { get; set; }

    public DateTime? TroopMemDateRegistered { get; set; }

    public string? ApplicationUserId { get; set; }

    public virtual ApplicationUser? ApplicationUser { get; set; }

    public virtual TroopMemberScoutLevel? TroopMemScoutLevel { get; set; }
}
