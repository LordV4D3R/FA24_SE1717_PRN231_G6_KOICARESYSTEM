namespace KoiCareSys.Data.DTO
{
    public class KoiRecordDTO
    {
        public Guid KoiId { get; set; }
        public decimal Weight { get; set; }
        public required string Physique { get; set; }
        public decimal Length { get; set; }
        public DateTime RecordAt { get; set; }
        public Guid DevelopmentStageId { get; set; }
    }
}
