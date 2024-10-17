using KoiCareSys.Data.DTO;
using KoiCareSys.Serivice.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiCareSys.Service.Service.Interface
{
    public interface  IProductService
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(Guid code);
        Task<IBusinessResult> DeleteById(Guid id);
        Task<IBusinessResult> Save(ProductDTO unitDTO);
    }
}
