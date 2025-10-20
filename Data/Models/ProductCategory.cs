using System;
using System.Collections.Generic;

namespace IA_AbansiBabayiSystemBlazor.Data.Models;

public partial class ProductCategory
{
    public int ProductCategoryId { get; set; }

    public string? ProductCategoryName { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
