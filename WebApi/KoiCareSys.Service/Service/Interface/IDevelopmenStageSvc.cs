using KoiCareSys.Data.DTO;
using KoiCareSys.Serivice.Base;

namespace KoiCareSys.Service.Service.Interface
{
    public interface IDevelopmenStageSvc
    {
        public Task<IBusinessResult> GetAllDevelopmenStage();
        public Task<IBusinessResult> GetDevelopmenStageById(Guid id);
        public Task<IBusinessResult> AddDevelopmenStage(DevelopmentStageDTO request);
        public Task<IBusinessResult> UpdateDevelopmenStage(DevelopmentStageUpdateDTO request);
        public Task<IBusinessResult> DeleteDevelopmenStage(Guid id);
    }
}
