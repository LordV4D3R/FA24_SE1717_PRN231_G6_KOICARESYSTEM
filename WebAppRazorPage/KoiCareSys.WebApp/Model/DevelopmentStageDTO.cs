
namespace KoiCareSys.WebApp.Model
{
    public class ApiResponse<T>
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }


    public class DevelopmentStageDTO
    {
        public required Guid Id { get; set; }
        public required string StageName { get; set; }
        public decimal RequiredFoodAmount { get; set; }
    }

    public class DevelopmentStageCreateDTO
    {
        public required string StageName { get; set; }
        public decimal RequiredFoodAmount { get; set; }
    }
}
