using KoiCareSys.Data.DTO;
using KoiCareSys.Serivice.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiCareSys.Service.Service.Interface
{
     public interface  IOrderService
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(Guid unitId);
        Task<IBusinessResult> Create(OrderDTO request);
        Task<IBusinessResult>  Update(OrderDTO request);
    }
}
