using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Health_Insurance.Models;

namespace Health_Insurance.Areas.AdminDashboard.Controllers
{
    [Area("AdminDashboard")]
    public class SahtakSubscriptionsController : Controller
    {
        private readonly ModelContext _context;

        public SahtakSubscriptionsController(ModelContext context)
        {
            _context = context;
        }

        // GET: AdminDashboard/SahtakSubscriptions
        public async Task<IActionResult> Index()
        {
            ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
            ViewData["FirstName"] = HttpContext.Session.GetString("FirstName");
            ViewData["LastName"] = HttpContext.Session.GetString("LastName");
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("SignIn", "Account", new { area = "" });
            }
            return _context.SahtakSubscriptions != null ? 
                          View(await _context.SahtakSubscriptions.ToListAsync()) :
                          Problem("Entity set 'ModelContext.SahtakSubscriptions'  is null.");
        }

        // GET: AdminDashboard/SahtakSubscriptions/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
            ViewData["FirstName"] = HttpContext.Session.GetString("FirstName");
            ViewData["LastName"] = HttpContext.Session.GetString("LastName");
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("SignIn", "Account", new { area = "" });
            }
            if (id == null || _context.SahtakSubscriptions == null)
            {
                return NotFound();
            }

            var sahtakSubscription = await _context.SahtakSubscriptions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sahtakSubscription == null)
            {
                return NotFound();
            }

            return View(sahtakSubscription);
        }

        // GET: AdminDashboard/SahtakSubscriptions/Create
        public IActionResult Create()
        {
            ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
            ViewData["FirstName"] = HttpContext.Session.GetString("FirstName");
            ViewData["LastName"] = HttpContext.Session.GetString("LastName");
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("SignIn", "Account", new { area = "" });
            }
            return View();
        }

        // POST: AdminDashboard/SahtakSubscriptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,Period")] SahtakSubscription sahtakSubscription)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sahtakSubscription);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sahtakSubscription);
        }

        // GET: AdminDashboard/SahtakSubscriptions/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
            ViewData["FirstName"] = HttpContext.Session.GetString("FirstName");
            ViewData["LastName"] = HttpContext.Session.GetString("LastName");
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("SignIn", "Account", new { area = "" });
            }
            if (id == null || _context.SahtakSubscriptions == null)
            {
                return NotFound();
            }

            var sahtakSubscription = await _context.SahtakSubscriptions.FindAsync(id);
            if (sahtakSubscription == null)
            {
                return NotFound();
            }
            return View(sahtakSubscription);
        }

        // POST: AdminDashboard/SahtakSubscriptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Name,Description,Price,Period")] SahtakSubscription sahtakSubscription)
        {
            if (id != sahtakSubscription.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sahtakSubscription);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SahtakSubscriptionExists(sahtakSubscription.Id))
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
            return View(sahtakSubscription);
        }

        // GET: AdminDashboard/SahtakSubscriptions/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
            ViewData["FirstName"] = HttpContext.Session.GetString("FirstName");
            ViewData["LastName"] = HttpContext.Session.GetString("LastName");
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("SignIn", "Account", new { area = "" });
            }
            if (id == null || _context.SahtakSubscriptions == null)
            {
                return NotFound();
            }

            var sahtakSubscription = await _context.SahtakSubscriptions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sahtakSubscription == null)
            {
                return NotFound();
            }

            return View(sahtakSubscription);
        }

        // POST: AdminDashboard/SahtakSubscriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.SahtakSubscriptions == null)
            {
                return Problem("Entity set 'ModelContext.SahtakSubscriptions'  is null.");
            }
            var sahtakSubscription = await _context.SahtakSubscriptions.FindAsync(id);
            if (sahtakSubscription != null)
            {
                _context.SahtakSubscriptions.Remove(sahtakSubscription);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SahtakSubscriptionExists(decimal id)
        {
          return (_context.SahtakSubscriptions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
