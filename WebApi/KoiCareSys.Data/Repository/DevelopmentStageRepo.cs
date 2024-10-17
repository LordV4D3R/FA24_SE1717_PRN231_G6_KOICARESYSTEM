using KoiCareSys.Data.Base;
using KoiCareSys.Data.DAO;
using KoiCareSys.Data.DTO;
using KoiCareSys.Data.Models;
using KoiCareSys.Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KoiCareSys.Data.Repository
{
    public class DevelopmentStageRepo : GenericRepository<DevelopmentStageDTO>, IDevelopmentStageRepo
    {

        private readonly DevelopmentStageDAO _dao;

        public DevelopmentStageRepo()
        {
            this._dao ??= new DevelopmentStageDAO();
        }

        public async Task<bool> AddDevelopmentStageAsync(DevelopmentStageDTO developmentStage)
        {
            DevelopmentStage create = new DevelopmentStage()
            {
                Id = Guid.NewGuid(),
                StageName = developmentStage.StageName,
                RequiredFoodAmount = developmentStage.RequiredFoodAmount,
            };
            int result = await _dao.CreateAsync(create);
            return result > 0;

        }

        public async Task<IEnumerable<DevelopmentStage>> GetAllDevelopmentStagesAsync()
        {
            return await _dao.GetAllAsync();
        }

        public async Task<IEnumerable<DevelopmentStage>> GetAllDevelopmentStagesByKewordsAsync(string keyword)
        {
            return await _dao.GetAllAsync(x => x.StageName.Contains(keyword));
        }

        public async Task<DevelopmentStage> GetDevelopmentStageByIdAsync(Guid id)
        {
            return await _dao.GetByIdAsync(id);
        }

        public async Task<bool> RemoveDevelopmentStage(Guid id)
        {
            DevelopmentStage found = _dao.GetById(id);
            if (found == null)
            {
                return false;
            }

            return await _dao.RemoveAsync(found);

        }

        public async Task<bool> UpdateDevelopmentStageAsync(DevelopmentStageUpdateDTO developmentStage)
        {

            DevelopmentStage update = new DevelopmentStage()
            {
                Id = developmentStage.Id,
                StageName = developmentStage.StageName,
                RequiredFoodAmount = developmentStage.RequiredFoodAmount,
            };

            int result = await _dao.UpdateAsync(update);
            return result > 0;
        }
    }
}
