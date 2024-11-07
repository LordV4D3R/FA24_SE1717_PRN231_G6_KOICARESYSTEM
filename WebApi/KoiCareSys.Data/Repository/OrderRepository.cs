using KoiCareSys.Data.Base;
using KoiCareSys.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiCareSys.Data.Repository
{
    public class OrderRepository :GenericRepository<Order>
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository() { }
        public OrderRepository(ApplicationDbContext context) { _context = context; }
        public async Task<Order> GetByIdAsync(Guid id)
        {
            return await _context.Orders
                          .Include(o => o.OrderDetails) 
                          .FirstOrDefaultAsync(o => o.Id == id);
        }
    }
}
