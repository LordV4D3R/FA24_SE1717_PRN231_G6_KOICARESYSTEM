using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiCareSys.Data.DTO
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }
   
        public string? ImgUrl { get; set; }


        public string? Description { get; set; }

       
        public Enums.ProductStatus Status { get; set; }
    }
}
