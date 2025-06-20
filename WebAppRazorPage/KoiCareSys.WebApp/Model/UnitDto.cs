﻿namespace KoiCareSys.WebApp.Model
{
    public class UnitDto
    {
        public Guid UnitId { get; set; }

        public string Name { get; set; }

        public string UnitOfMeasure { get; set; }

        public string FullName { get; set; }

        public string Information { get; set; }

        public decimal? MinValue { get; set; }

        public decimal? MaxValue { get; set; }
    }
}
