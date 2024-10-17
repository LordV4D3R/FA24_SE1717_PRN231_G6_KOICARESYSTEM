namespace KoiCareSys.Data.DTO
{
    public class DevelopmentStageDTO
    {
        public required string StageName { get; set; }
        public decimal RequiredFoodAmount { get; set; }
    }

    public class DevelopmentStageUpdateDTO
    {
        public required Guid Id { get; set; }
        public required string StageName { get; set; }
        public decimal RequiredFoodAmount { get; set; }
    }
}
