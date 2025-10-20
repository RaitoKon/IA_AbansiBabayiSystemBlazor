using System;
using System.Collections.Generic;

namespace IA_AbansiBabayiSystemBlazor.Data.Models;

public partial class ProductSale
{
    public int ProductSaleId { get; set; }

    public int? ProductId { get; set; }

    public decimal? ProductSalePrice { get; set; }

    public int? ProductSaleQuantity { get; set; }

    public DateTime? ProductSaleDate { get; set; }

    public decimal? TotalCost { get; set; }

    public virtual Product? Product { get; set; }
}
