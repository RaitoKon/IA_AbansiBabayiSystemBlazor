using System;
using System.Collections.Generic;

namespace IA_AbansiBabayiSystemBlazor.Data.Models;

public partial class EventsOrAnnouncementsTarget
{
    public int EventsOrAnnouncementsTargetId { get; set; }

    public int? EventsOrAnnouncementsId { get; set; }

    public string? TargetPeople { get; set; }

    public virtual AddEventsOrAnnouncement? EventsOrAnnouncements { get; set; }
}
