using KoiCareSys.Data.DTO;
using KoiCareSys.Serivice.Base;

namespace KoiCareSys.Service.Service.Interface
{
    public interface IUnitService
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(Guid code);
        Task<IBusinessResult> Create(UnitDTO unitDTO);
        Task<IBusinessResult> Update(UnitDTO unitDTO);
        Task<IBusinessResult> DeleteById(Guid id);
    }
}
