namespace KoiCareSys.WebApp.Model
{
    public class MeasurementDto
    {
        public Guid MeasurementId { get; set; }

        public Guid PondId { get; set; }

        public DateTime DateRecord { get; set; }

        public List<MeasureDataDto>? MeasureData { get; set; }

        public string? Note { get; set; }
    }
}
