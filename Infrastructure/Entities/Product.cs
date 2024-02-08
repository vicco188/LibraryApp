using System;
using System.Collections.Generic;

namespace Infrastructure.Entities;

public partial class Product
{
    public int ArticleNumber { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int CategoryId { get; set; }

    public int ManufacturerId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Manufacturer Manufacturer { get; set; } = null!;
}
