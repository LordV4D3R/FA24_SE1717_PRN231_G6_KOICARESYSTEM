using KoiCareSys.Data.DTO;
using KoiCareSys.Serivice.Base;

namespace KoiCareSys.Service.Service.Interface
{
    public interface IMeasurementService
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(Guid code);
        Task<IBusinessResult> Create(MeasurementDTO measurementDTO);
        Task<IBusinessResult> Update(MeasurementDTO measurementDTO);
        Task<IBusinessResult> DeleteById(Guid id);
    }
}
