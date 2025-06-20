﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KoiCareSys.Data.Models;

[Table("koi_record")]
public partial class KoiRecord
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("record_name")]
    public string RecordName { get; set; }

    [Column("color")]
    public string Color { get; set; }

    [Column("price")]
    public double Price { get; set; }

    [Column("description")]
    public string Description { get; set; }

    [Column("health_issue")]
    public string HealthIssue { get; set; }

    [Column("weight")]
    public decimal? Weight { get; set; }

    [Column("record_at")]
    public DateTime RecordAt { get; set; } = DateTime.Now;

    [Column("length")]
    public decimal? Length { get; set; }

    [Column("physique")]
    public string Physique { get; set; }

    [Column("koi_id")]
    [ForeignKey("Koi")]
    public Guid KoiId { get; set; }
    public virtual Koi Koi { get; set; }

    [Column("development_stage_id")]
    [ForeignKey("DevelopmentStage")]
    public Guid DevelopmentStageId { get; set; }
    public virtual DevelopmentStage DevelopmentStage { get; set; }
}