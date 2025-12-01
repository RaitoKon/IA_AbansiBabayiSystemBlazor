using System;
using System.Collections.Generic;

namespace IA_AbansiBabayiSystemBlazor.Data.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public string? ProductDescription { get; set; }

    public int? ProductCategoryId { get; set; }

    public decimal? ProductPrice { get; set; }

    public string? ProductImagePath { get; set; }

    public virtual ProductCategory? ProductCategory { get; set; }

    public virtual ProductStock? ProductStock { get; set; }
}
