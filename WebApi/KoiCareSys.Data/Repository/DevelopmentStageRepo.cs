using KoiCareSys.Data.Base;
using KoiCareSys.Data.DAO;
using KoiCareSys.Data.DTO;
using KoiCareSys.Data.Models;
using KoiCareSys.Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KoiCareSys.Data.Repository
{
    public class DevelopmentStageRepo : GenericRepository<DevelopmentStage>, IDevelopmentStageRepo
    {
        public DevelopmentStageRepo(ApplicationDbContext context) : base(context)
        {
        }
    }
}
