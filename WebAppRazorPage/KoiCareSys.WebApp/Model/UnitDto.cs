namespace KoiCareSys.WebApp.Model
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

        public decimal? IdealValue { get; set; }

        public decimal? WarningThreshold { get; set; }

        public decimal? CriticalThreshold { get; set; }

        public bool IsActive { get; set; }

        public string Description { get; set; }

        public string AlertMessage { get; set; }
    }
}
