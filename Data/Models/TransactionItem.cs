using System;
using System.Collections.Generic;

namespace IA_AbansiBabayiSystemBlazor.Data.Models;

public partial class TransactionItem
{
    public int TransactionItemsId { get; set; }

    public int? TransactionId { get; set; }

    public int? ProductId { get; set; }

    public int? ProductQuantity { get; set; }

    public decimal? TransactionSubtotal { get; set; }

    public virtual Product? Product { get; set; }

    }
