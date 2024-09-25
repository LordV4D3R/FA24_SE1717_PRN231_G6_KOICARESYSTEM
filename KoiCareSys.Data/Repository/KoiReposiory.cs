using KoiCareSys.Data.Base;
using KoiCareSys.Data.Models;

namespace KoiCareSys.Data.Repository
{
    public class KoiReposiory : GenericRepository<Koi>
    {
        public KoiReposiory() { }
        public KoiReposiory(ApplicationDbContext context) => _context = context;
    }
}
