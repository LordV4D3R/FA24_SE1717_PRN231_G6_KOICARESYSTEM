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
        Task<IBusinessResult> GetById(string KoiNo);
        //Task<IBusinessResult> Create(Koi koi);
        //Task<IBusinessResult> Update(Koi koi);
        Task<IBusinessResult> Save(Koi koi);
        Task<IBusinessResult> DeleteById(string KoiNo);
    }
}
