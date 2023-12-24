using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Health_Insurance.Models;

namespace Health_Insurance.Controllers
{
    public class SahtakBanksController : Controller
    {
        private readonly ModelContext _context;

        public SahtakBanksController(ModelContext context)
        {
            _context = context;
        }

        // GET: SahtakBanks
        public async Task<IActionResult> Index()
        {
              return _context.SahtakBanks != null ? 
                          View(await _context.SahtakBanks.ToListAsync()) :
                          Problem("Entity set 'ModelContext.SahtakBanks'  is null.");
        }

        // GET: SahtakBanks/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.SahtakBanks == null)
            {
                return NotFound();
            }

            var sahtakBank = await _context.SahtakBanks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sahtakBank == null)
            {
                return NotFound();
            }

            return View(sahtakBank);
        }

        // GET: SahtakBanks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SahtakBanks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AccountName,Amount,Password")] SahtakBank sahtakBank)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sahtakBank);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sahtakBank);
        }

        // GET: SahtakBanks/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.SahtakBanks == null)
            {
                return NotFound();
            }

            var sahtakBank = await _context.SahtakBanks.FindAsync(id);
            if (sahtakBank == null)
            {
                return NotFound();
            }
            return View(sahtakBank);
        }

        // POST: SahtakBanks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,AccountName,Amount,Password")] SahtakBank sahtakBank)
        {
            if (id != sahtakBank.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sahtakBank);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SahtakBankExists(sahtakBank.Id))
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
            return View(sahtakBank);
        }

        // GET: SahtakBanks/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.SahtakBanks == null)
            {
                return NotFound();
            }

            var sahtakBank = await _context.SahtakBanks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sahtakBank == null)
            {
                return NotFound();
            }

            return View(sahtakBank);
        }

        // POST: SahtakBanks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.SahtakBanks == null)
            {
                return Problem("Entity set 'ModelContext.SahtakBanks'  is null.");
            }
            var sahtakBank = await _context.SahtakBanks.FindAsync(id);
            if (sahtakBank != null)
            {
                _context.SahtakBanks.Remove(sahtakBank);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SahtakBankExists(decimal id)
        {
          return (_context.SahtakBanks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
