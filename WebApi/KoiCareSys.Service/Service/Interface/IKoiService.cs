using KoiCareSys.Data.DTO;
using KoiCareSys.Data.Models;
using KoiCareSys.Serivice.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiCareSys.Service.Service.Interface
{
    public interface IKoiService
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(Guid koiId);
        Task<IBusinessResult> Save(Guid koiId = default, KoiDTO dto = default);
        Task<IBusinessResult> DeleteById(Guid koiId);
    }
}
