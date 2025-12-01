using System;
using System.Collections.Generic;

namespace IA_AbansiBabayiSystemBlazor.Data.Models;

public partial class ProductIssue
{
    public int ProductIssueId { get; set; }

    public int? TransactionItemsId { get; set; }

    public string? OldTransactionNo { get; set; }

    public string? Description { get; set; }
}
