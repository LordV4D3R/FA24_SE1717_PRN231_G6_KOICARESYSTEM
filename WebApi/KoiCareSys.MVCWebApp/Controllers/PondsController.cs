using KoiCareSys.Data;
using KoiCareSys.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace KoiCareSys.MVCWebApp.Controllers
{
    public class PondsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PondsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ponds
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Ponds.Include(p => p.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Ponds/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pond = await _context.Ponds
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pond == null)
            {
                return NotFound();
            }

            return View(pond);
        }

        // GET: Ponds/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email");
            return View();
        }

        // POST: Ponds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PondName,Volume,Depth,DrainCount,SkimmerCount,PumpCapacity,ImgUrl,Note,Description,Status,IsQualified,UserId")] Pond pond)
        {
            if (ModelState.IsValid)
            {
                pond.Id = Guid.NewGuid();
                _context.Add(pond);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", pond.UserId);
            return View(pond);
        }

        // GET: Ponds/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pond = await _context.Ponds.FindAsync(id);
            if (pond == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", pond.UserId);
            return View(pond);
        }

        // POST: Ponds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,PondName,Volume,Depth,DrainCount,SkimmerCount,PumpCapacity,ImgUrl,Note,Description,Status,IsQualified,UserId")] Pond pond)
        {
            if (id != pond.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pond);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PondExists(pond.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", pond.UserId);
            return View(pond);
        }

        // GET: Ponds/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pond = await _context.Ponds
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pond == null)
            {
                return NotFound();
            }

            return View(pond);
        }

        // POST: Ponds/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var pond = await _context.Ponds.FindAsync(id);
            if (pond != null)
            {
                _context.Ponds.Remove(pond);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PondExists(Guid id)
        {
            return _context.Ponds.Any(e => e.Id == id);
        }
    }
}
