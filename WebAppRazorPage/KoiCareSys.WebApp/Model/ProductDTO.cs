using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using KoiCareSys.WebApp.Model.Enum;

namespace KoiCareSys.WebApp.Model
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string? ImgUrl { get; set; }


        public string? Description { get; set; }
        public ProductStatus Status { get; set; }

       
        //public DateTime CreateDate { get; set; }

        //public DateTime UpdateDate { get; set; }
    }
}
