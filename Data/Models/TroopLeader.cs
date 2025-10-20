using System;
using System.Collections.Generic;

namespace IA_AbansiBabayiSystemBlazor.Data.Models
{
    public partial class TroopLeader
    {
        public int LeaderId { get; set; }
        public int? LeaderPositionId { get; set; }
        public string? UserRole { get; set; }
        public string? LeaderTorNt { get; set; }
        public string? LeaderRegStatus { get; set; }
        public string? LeaderLname { get; set; }
        public string? LeaderFname { get; set; }
        public string? LeaderMname { get; set; }
        public DateTime? LeaderBirthdate { get; set; }
        public string? LeaderBeneficiary { get; set; }
        public string? LeaderEmail { get; set; }
        public string? LeaderRegisteredEmail { get; set; }
        public DateTime? LeaderDateRegistered { get; set; }
        public int? LeaderTroopNo { get; set; }

        // ✅ Use ApplicationUser instead of AspNetUser
        public string? ApplicationUserId { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }
        public virtual LeaderPosition? LeaderPosition { get; set; }
        public virtual ICollection<TroopInformation> TroopInformations { get; set; } = new List<TroopInformation>();
    }
}
