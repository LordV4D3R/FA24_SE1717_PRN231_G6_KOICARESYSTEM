using KoiCareSys.Data.Base;
using KoiCareSys.Data.Models;
using KoiCareSys.Data.Repository.Interface;

namespace KoiCareSys.Data.Repository
{
    public class KoiRecordRepo : GenericRepository<KoiRecord>, IKoiRecordRepo
    {
        public KoiRecordRepo(ApplicationDbContext context) : base(context)
        {
        }
    }
}
