using KoiCareSys.Data.Base;
using KoiCareSys.Data.DAO;
using KoiCareSys.Data.Models;
using KoiCareSys.Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KoiCareSys.Data.Repository
{
    public class PondRepository : GenericRepository<Pond> ,IPondRepository
    {
        private readonly PondDAO _dao;
        public PondRepository() 
        {
            _dao ??= new PondDAO();
        }

        public async Task<Pond> GetPond(Guid id)
        {
            return await GetByIdAsync(id);
        }

        public async Task<IEnumerable<Pond>> GetPonds()
        {
            return await GetAllAsync();
        }

        public async Task<IEnumerable<Pond>> GetAllPond(string search)
        {
            Expression<Func<Pond, bool>> predicate = x => x.PondName.Contains(search) || x.Description.Contains(search);
            IQueryable<Pond> query = _dbSet.Where(predicate);
            return query.AsNoTracking().AsEnumerable();
        }
    }
}
