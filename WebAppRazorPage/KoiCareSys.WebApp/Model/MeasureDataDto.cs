namespace KoiCareSys.WebApp.Model
{
    public class MeasureDataDto
    {
        public Guid MeasureDataId { get; set; }

        public decimal? Volume { get; set; }

        public Guid UnitId { get; set; }
    }
}
