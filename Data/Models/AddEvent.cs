using System;
using System.Collections.Generic;

namespace IA_AbansiBabayiSystemBlazor.Data.Models;

public partial class AddEvent
{
    public int EventId { get; set; }

    public string? EventTitle { get; set; }

    public string? EventDescription { get; set; }

    public DateTime? EventDate { get; set; }

    public string? EventImagePath { get; set; }

    public string? EventLocation { get; set; }
}
