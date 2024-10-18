using KoiCareSys.Data.DTO;
using KoiCareSys.Data.Models;

namespace KoiCareSys.Data.Repository.Interface
{
    public interface IKoiRecordRepo
    {
        public Task<IEnumerable<KoiRecord>> GetAllKoiRecordsByKeywordAsync(string keyword);

        public Task<IEnumerable<KoiRecord>> GetAllKoiRecordsAsync();

        public Task<KoiRecord> GetKoiRecordByIdAsync(Guid id);

        public Task<bool> AddKoiRecordAsync(KoiRecordDTO koiRecord);

        public Task<bool> UpdateKoiRecordAsync(KoiRecordUpdateDTO koiRecord);
        public Task<bool> RemoveKoiRecord(Guid id);
    }
}
