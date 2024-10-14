using KoiCareSys.Data.DTO;
using KoiCareSys.Data.Models;

namespace KoiCareSys.Data.Repository.Interface
{
    public interface IDevelopmentStageRepo
    {
        public Task<IEnumerable<DevelopmentStage>> GetAllDevelopmentStagesAsync();

        public Task<DevelopmentStage> GetDevelopmentStageByIdAsync(int id);

        public Task<bool> AddDevelopmentStageAsync(DevelopmentStageDTO developmentStage);

        public Task<bool> UpdateDevelopmentStageAsync(DevelopmentStageUpdateDTO developmentStage);
        public Task<bool> RemoveDevelopmentStage(int id);
    }
}
