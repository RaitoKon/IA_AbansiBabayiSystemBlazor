using System;
using System.Collections.Generic;

namespace IA_AbansiBabayiSystemBlazor.Data.Models;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public string? TransactionType { get; set; }

    public string? TransactionNo { get; set; }

    public decimal? TransactionTotalCost { get; set; }

    public DateTime? TransactionDateAdded { get; set; }

    public string TransactionDateFormatted =>
    TransactionDateAdded?.ToString("MMMM dd, yyyy h:mm") + " " +
    TransactionDateAdded?.ToString("tt").ToUpper() ?? "";

}
