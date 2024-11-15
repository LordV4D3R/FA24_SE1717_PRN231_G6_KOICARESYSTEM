namespace KoiCareSys.MVCWebApp.Models
{
    public class FeedingScheduleDTO
    {
        public decimal? Foodcaculate { get; set; }
        public DateTime FeedAt { get; set; }
        public decimal? FoodAmount { get; set; }
        public string? FeedBy { get; set; }
        public decimal? Temperature { get; set; }
        public string? FoodType { get; set; }
        public string? Note { get; set; }
        public Guid PondId { get; set; }
    }
}
