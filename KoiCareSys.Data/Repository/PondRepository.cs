using KoiCareSys.Data.Base;
using KoiCareSys.Data.DAO;
using KoiCareSys.Data.Models;
using KoiCareSys.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
