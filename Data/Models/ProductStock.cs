using System;
using System.Collections.Generic;

namespace IA_AbansiBabayiSystemBlazor.Data.Models;

public partial class ProductStock
{
    public int StockId { get; set; }

    public int? ProductId { get; set; }

    public int? SupplierId { get; set; }

    public decimal? PurchasePrice { get; set; }

    public int? StockMonthlyRemaining { get; set; }

    public int? StockBefore { get; set; }

    public int? StockAdded { get; set; }

    public int? StockTotal { get; set; }

    public DateTime? StockDateAdded { get; set; }

    public virtual Product? Product { get; set; }
    
    public virtual ProductSupplier? Supplier { get; set; }
}
