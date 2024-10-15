using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiCareSys.Data.DTO
{
    public class PondDTO
    {
        public String PondName { get; set; } = String.Empty;
        public decimal? Volume { get; set; }
        public decimal? Depth { get; set; }
        public int? DrainCount { get; set; }
        public int? SkimmerCount { get; set; }
        public decimal? PumpCapacity { get; set; }
        public string ImgUrl { get; set; }
        public string Note { get; set; }
        public string Description { get; set; }
        public Enums.PondStatus Status { get; set; }
        public bool? IsQualified { get; set; }
        public Guid UserId { get; set; }
    }
}
