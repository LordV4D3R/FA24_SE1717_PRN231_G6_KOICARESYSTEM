using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiCareSys.Data.Repository
{
    public class OrderDetailRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderDetailRepository() { }
        public OrderDetailRepository(ApplicationDbContext context) { _context = context; }

    }
}
