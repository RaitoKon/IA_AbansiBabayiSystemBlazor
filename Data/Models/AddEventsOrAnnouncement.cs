using System;
using System.Collections.Generic;

namespace IA_AbansiBabayiSystemBlazor.Data.Models;

public partial class AddEventsOrAnnouncement
{
    public int EventsOrAnnouncementsId { get; set; }

    public string? EventOrAnnouncementType { get; set; }

    public string? EventsOrAnnouncementsTitle { get; set; }

    public string? EventsOrAnnouncementsDescription { get; set; }

    public DateTime? EventsOrAnnouncementsDateFrom { get; set; }

    public DateTime? EventsOrAnnouncementsDateTo { get; set; }

    public string? EventsOrAnnouncementsImagePath { get; set; }

    public string? EventsOrAnnouncementsLocation { get; set; }

    public DateTime? DatePosted { get; set; }

    public virtual ICollection<EventsOrAnnouncementsTarget> EventsOrAnnouncementsTargets { get; set; } = new List<EventsOrAnnouncementsTarget>();
}
