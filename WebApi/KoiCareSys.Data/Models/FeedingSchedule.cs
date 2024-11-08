using System;
using System.Collections.Generic;

namespace KoiCareSys.Data.Models;

public partial class FeedingSchedule
{
    public Guid Id { get; set; }

    public decimal? Foodcaculate { get; set; }

    public DateTime FeedAt { get; set; }

    public decimal? FoodAmount { get; set; }

    public string? FeedBy { get; set; }

    public decimal? Temperature { get; set; }

    public string? FoodType { get; set; }

    public string? Note { get; set; }

    public Guid PondId { get; set; }

    public virtual Pond Pond { get; set; } = null!;
}
