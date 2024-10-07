using KoiCareSys.Data.DTO;
using KoiCareSys.Serivice.Base;

namespace KoiCareSys.Service.Service.Interface
{
    public interface IMeasurementService
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(string code);
        Task<IBusinessResult> Save(MeasurementDTO measurementDTO);
        Task<IBusinessResult> DeleteById(string id);
    }
}
