using System;
using System.Collections.Generic;

namespace IA_AbansiBabayiSystemBlazor.Data;

public partial class ScoutJoinedEvent
{
    public int ScoutJoinedEventsId { get; set; }

    public int? EventsOrAnnouncementId { get; set; }

    public int? TroopMemId { get; set; }

    public string? PaymentStatus { get; set; }

    public DateTime? DateModified { get; set; }
}
