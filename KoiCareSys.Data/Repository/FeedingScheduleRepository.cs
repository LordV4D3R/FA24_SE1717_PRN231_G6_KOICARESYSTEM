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
    public class FeedingScheduleRepository : GenericRepository<FeedingSchedule>, IFeedingScheduleRepository
    {
        private readonly FeedingScheduleDAO _dao;
        public FeedingScheduleRepository()
        {
            _dao ??= new FeedingScheduleDAO();
        }

        public async Task<FeedingSchedule> GetFeedingSchedule(Guid id)
        {
            return await GetByIdAsync(id);
        }

        public async Task<IEnumerable<FeedingSchedule>> GetAllFeedingSchedules()
        {
            return await GetAllAsync();
        }

        public bool CreateFeedingSchedule(FeedingSchedule feedingSchedule)
        {
            FeedingSchedule create = new FeedingSchedule()
            {
                FeedAt = feedingSchedule.FeedAt,
                FoodAmount = feedingSchedule.FoodAmount,
                KoiId = feedingSchedule.KoiId

            };
            _dao.Create(create);
            return true;
        }


    }
}
