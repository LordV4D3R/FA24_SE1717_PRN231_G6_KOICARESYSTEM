using KoiCareSys.Data.Base;
using KoiCareSys.Data.Models;
using KoiCareSys.Data.Repository.Interface;

namespace KoiCareSys.Data.Repository
{
    public class DevelopmentStageRepo : GenericRepository<DevelopmentStage>, IDevelopmentStageRepo
    {
        public DevelopmentStageRepo(ApplicationDbContext context) : base(context)
        {
        }
    }
}
