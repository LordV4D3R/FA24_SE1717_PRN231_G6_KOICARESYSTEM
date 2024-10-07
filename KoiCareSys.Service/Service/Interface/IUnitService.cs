using KoiCareSys.Data.DTO;
using KoiCareSys.Serivice.Base;

namespace KoiCareSys.Service.Service.Interface
{
    public interface IUnitService
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(string code);
        Task<IBusinessResult> Save(UnitDTO unitDTO);
        Task<IBusinessResult> DeleteById(string id);
    }
}
