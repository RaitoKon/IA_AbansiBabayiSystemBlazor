using System;
using System.Collections.Generic;

namespace IA_AbansiBabayiSystemBlazor.Data.Models;

public partial class ProductPurchase
{
    public int ProductPurchaseId { get; set; }

    public int? ProductId { get; set; }

    public decimal? ProductPurchasePrice { get; set; }

    public int? ProductPurchaseQuantity { get; set; }

    public DateTime? ProductPurchaseDate { get; set; }

    public decimal? TotalCost { get; set; }

    public virtual Product? Product { get; set; }
}
