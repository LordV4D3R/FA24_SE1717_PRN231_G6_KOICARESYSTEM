using KoiCareSys.Data.DTO;
using KoiCareSys.Serivice.Base;

namespace KoiCareSys.Service.Service.Interface
{
    public interface IKoiRecordSvc
    {
        public Task<IBusinessResult> GetAllKoiRecordsByKeywordAsync(string keyword);
        public Task<IBusinessResult> GetAllKoiRecordsAsync();
        public Task<IBusinessResult> GetKoiRecordByIdAsync(Guid id);
        public Task<IBusinessResult> AddKoiRecordAsync(KoiRecordDTO request);
        public Task<IBusinessResult> UpdateKoiRecordAsync(KoiRecordUpdateDTO request);
        public Task<IBusinessResult> RemoveKoiRecord(Guid id);

    }
}
