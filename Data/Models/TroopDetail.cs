using System;
using System.Collections.Generic;

namespace IA_AbansiBabayiSystemBlazor.Data.Models;

public partial class TroopDetail
{
    public int TroopDetailsId { get; set; }

    public string? TroopName { get; set; }

    public string? TroopAgeLevel { get; set; }

    public string? TroopAddress { get; set; }

    public string? TroopSponsoringGroup { get; set; }

    public int? TroopTelNo { get; set; }

    public string? TroopMailingAddress { get; set; }

    public string? TroopDistrictCommittee { get; set; }

    public string? TroopBarangayCommittee { get; set; }

    public DateTime? TroopBirthdate { get; set; }

    public string? TroopType { get; set; }

    public string? TroopStatus { get; set; }
}
