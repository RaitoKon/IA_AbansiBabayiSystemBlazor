using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IA_AbansiBabayiSystemBlazor.Data.Models;

public partial class TroopLeaderRegistration
{
    public int LeaderId { get; set; }

    [Required]
    public string? LeaderPosition { get; set; }

    [Required]
    public string? LeaderRole { get; set; }

    public int? CoLeaderTroopNumber { get; set; }

    [Required]
    public string? LeaderTorNT { get; set; }

    [Required]
    public string? LeaderRegStatus { get; set; }

    [Required]
    public string? LeaderLname { get; set; }

    [Required]
    public string? LeaderFname { get; set; }

    [Required]
    public string? LeaderMInitial { get; set; }

    [Required]
    public DateTime? LeaderBirthdate { get; set; }

    [Required]
    public string? LeaderBeneficiary { get; set; }

    [Required]
    public string? LeaderEmail { get; set; }

}
