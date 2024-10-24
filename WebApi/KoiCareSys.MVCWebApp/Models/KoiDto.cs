using KoiCareSys.MVCWebApp.Models.Enum;

namespace KoiCareSys.MVCWebApp.Models
{
    public class KoiDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Physique { get; set; }
        public int Age { get; set; }
        public double Length { get; set; }
        public KoiSex Sex { get; set; } // Assuming 1 = Male, 2 = Female
        public string Category { get; set; }
        public DateOnly InPondSince { get; set; }
        public decimal PurchasePrice { get; set; }
        public KoiStatus Status { get; set; } // Assuming 1 = Active, 0 = Inactive
        public string ImgUrl { get; set; }
        public string Origin { get; set; }
        public string Breed { get; set; }
        public Guid PondId { get; set; }
    }
}
