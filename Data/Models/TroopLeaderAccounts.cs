using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace IA_AbansiBabayiSystemBlazor.Data.Models;

public partial class TroopLeaderAccount
{
    public int AccountsId { get; set; }

    public int LeaderId { get; set; }

    public string Id { get; set; } = null!;

    [ForeignKey("LeaderId")]
    public RegisteredTroopLeader RegisteredTroopLeader { get; set; } = null!;

    [ForeignKey("Id")]
    public ApplicationUser ApplicationUser { get; set; } = null!;

}
