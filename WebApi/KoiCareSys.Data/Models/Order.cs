using System;
using System.Collections.Generic;

namespace KoiCareSys.Data.Models;

public partial class Order
{
    public Guid Id { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime? OrderDate { get; set; }

    public string? Status { get; set; }

    public decimal? Total { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
