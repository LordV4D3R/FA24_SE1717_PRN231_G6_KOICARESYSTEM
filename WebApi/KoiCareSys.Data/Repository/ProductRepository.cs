using KoiCareSys.Data.Base;
using KoiCareSys.Data.DAO;
using KoiCareSys.Data.Models;
using KoiCareSys.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiCareSys.Data.Repository
{
    public class ProductRepository : GenericRepository<Product>,IProductRepository
    {
        private readonly ProductDAO _productDAO;
        public ProductRepository() 
        {
            _productDAO = new ProductDAO();
        }
       
    }
}
