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
    public interface IFeedingScheduleService
    {
        Task<IBusinessResult> GetAll(String? search);
        Task<IBusinessResult> Create(FeedingScheduleDTO request);
        Task<IBusinessResult> GetById(Guid code);
        Task<IBusinessResult> CaculateFoodAmountByKoi(Guid pondId);
        Task<IBusinessResult> Update(FeedingSchedule feedingSchedule, FeedingScheduleDTO updateFeedingSchedule);
        Task<IBusinessResult> Delete(Guid code);
    }
}
