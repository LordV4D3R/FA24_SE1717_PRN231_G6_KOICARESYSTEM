using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace KoiCareSys.Data.DTO
{
    public class KoiDTO
    {
        [AllowNull]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string Physique { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public decimal Length { get; set; }

        [Required]
        [EnumDataType(typeof(Enums.KoiSex))]
        public Enums.KoiSex Sex { get; set; } = Enums.KoiSex.Unknown;

        public string Category { get; set; }

        [Required]
        public DateOnly InPondSince { get; set; }

        public decimal? PurchasePrice { get; set; }

        [EnumDataType(typeof(Enums.KoiStatus))]
        public Enums.KoiStatus Status { get; set; } = Enums.KoiStatus.Active;

        [Url]
        public string ImgUrl { get; set; }

        public string Origin { get; set; }

        public string Breed { get; set; }

        [Required]
        public Guid PondId { get; set; }
    }
}
