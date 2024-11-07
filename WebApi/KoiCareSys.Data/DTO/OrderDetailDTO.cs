using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiCareSys.Data.DTO
{
    public class OrderDetailDTO
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
