using KoiCareSys.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiCareSys.Data.Repository.Interface
{
    public interface IFeedingScheduleRepository
    {
        Task<IEnumerable<FeedingSchedule>> GetFeedingSchedules();
        Task<FeedingSchedule> GetFeedingSchedule(Guid id);
        Task<IEnumerable<FeedingSchedule>> GetAllFeedingSchedules(string? search);
        bool CreateFeedingSchedule(FeedingSchedule feedingSchedule);
        bool DeleteFeedingSchedule(Guid id);

    }
}
