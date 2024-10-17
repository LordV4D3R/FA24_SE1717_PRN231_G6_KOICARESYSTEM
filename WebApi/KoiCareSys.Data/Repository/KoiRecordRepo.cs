using KoiCareSys.Data.Base;
using KoiCareSys.Data.DAO;
using KoiCareSys.Data.DTO;
using KoiCareSys.Data.Models;
using KoiCareSys.Data.Repository.Interface;

namespace KoiCareSys.Data.Repository
{
    public class KoiRecordRepo : GenericRepository<KoiRecord>, IKoiRecordRepo
    {
        private readonly KoiRecordDAO _dao;

        public KoiRecordRepo()
        {
            this._dao ??= new KoiRecordDAO();
        }

        public async Task<bool> AddKoiRecordAsync(KoiRecordDTO koiRecord)
        {
            KoiRecord create = new KoiRecord()
            {
                Id = Guid.NewGuid(),
                KoiId = koiRecord.KoiId,
                Length = koiRecord.Length,
                Weight = koiRecord.Weight,
                Physique = koiRecord.Physique,
                RecordAt = koiRecord.RecordAt,
                DevelopmentStageId = koiRecord.DevelopmentStageId
            };
            int result = await _dao.CreateAsync(create);
            return result > 0;
        }

        public async Task<IEnumerable<KoiRecord>> GetAllKoiRecordsAsync()
        {
            return await _dao.GetAllAsync();
        }

        public async Task<IEnumerable<KoiRecord>> GetAllKoiRecordsByKeywordAsync(string keyword)
        {
            return await _dao.GetAllAsync(
                x => x.Physique.Contains(keyword)
            || x.Length.ToString().Contains(keyword)
            || x.Weight.ToString().Contains(keyword)
            || x.RecordAt.ToString().Contains(keyword)
            || x.DevelopmentStageId.ToString().Contains(keyword));

        }

        public async Task<KoiRecord> GetKoiRecordByIdAsync(Guid id)
        {
            return await _dao.GetByIdAsync(id);
        }

        public async Task<bool> RemoveKoiRecord(Guid id)
        {
            KoiRecord found = _dao.GetById(id);
            if (found == null)
            {
                return false;
            }
            return await _dao.RemoveAsync(found);

        }

        public async Task<bool> UpdateKoiRecordAsync(KoiRecordUpdateDTO koiRecord)
        {
            KoiRecord update = new KoiRecord()
            {
                Id = koiRecord.Id,
                KoiId = koiRecord.KoiId,
                Length = koiRecord.Length,
                Weight = koiRecord.Weight,
                Physique = koiRecord.Physique,
                RecordAt = koiRecord.RecordAt,
                DevelopmentStageId = koiRecord.DevelopmentStageId
            };
            int result = await _dao.UpdateAsync(update);
            return result > 0;
        }
    }
}
