using System;
using System.Collections.Generic;

namespace IA_AbansiBabayiSystemBlazor.Data.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public decimal? ProductPrice { get; set; }

    public int? ProductStock { get; set; }

    public int? ProductCategoryId { get; set; }

    public string? ProductDescription { get; set; }

    public string? ProductImagePath { get; set; }

    public virtual ProductCategory? ProductCategory { get; set; }

    public virtual ICollection<ProductPurchase> ProductPurchases { get; set; } = new List<ProductPurchase>();

    public virtual ICollection<ProductSale> ProductSales { get; set; } = new List<ProductSale>();
}
