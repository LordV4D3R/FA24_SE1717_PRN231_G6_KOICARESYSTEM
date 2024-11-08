using System;
using System.Collections.Generic;

namespace KoiCareSys.Data.Models;

public partial class Pond
{
    public Guid Id { get; set; }

    public string PondName { get; set; } = null!;

    public decimal Volume { get; set; }

    public decimal Depth { get; set; }

    public int? DrainCount { get; set; }

    public int? SkimmerCount { get; set; }

    public decimal? PumpCapacity { get; set; }

    public string? ImgUrl { get; set; }

    public string? Note { get; set; }

    public string? Description { get; set; }

    public int Status { get; set; }

    public bool? IsQualified { get; set; }

    public Guid UserId { get; set; }

    public virtual ICollection<FeedingSchedule> FeedingSchedules { get; set; } = new List<FeedingSchedule>();

    public virtual ICollection<Koi> Kois { get; set; } = new List<Koi>();

    public virtual ICollection<Measurement> Measurements { get; set; } = new List<Measurement>();

    public virtual User User { get; set; } = null!;
}
