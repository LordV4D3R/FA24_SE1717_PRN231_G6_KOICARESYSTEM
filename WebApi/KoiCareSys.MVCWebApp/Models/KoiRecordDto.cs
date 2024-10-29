namespace KoiCareSys.MVCWebApp.Models
{
    public class KoiRecordDto
    {
        public Guid Id { get; set; }
        public Guid KoiId { get; set; }
        public decimal Weight { get; set; }
        public required string Physique { get; set; }
        public decimal Length { get; set; }
        public required string RecordName { get; set; }
        public required string Color { get; set; }
        public double Price { get; set; }
        public required string Description { get; set; }
        public required string HealthIssue { get; set; }
        public DateTime RecordAt { get; set; }
        public Guid DevelopmentStageId { get; set; }
    }
}
