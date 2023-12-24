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
    public class SahtakContactUsController : Controller
    {
        private readonly ModelContext _context;

        public SahtakContactUsController(ModelContext context)
        {
            _context = context;
        }

        // GET: AdminDashboard/SahtakContactUs
        public async Task<IActionResult> Index()
        {
			ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
			ViewData["FirstName"] = HttpContext.Session.GetString("FirstName");
			ViewData["LastName"] = HttpContext.Session.GetString("LastName");
			if (HttpContext.Session.GetInt32("UserID") == null)
			{
				return RedirectToAction("SignIn", "Account", new { area = "" });
			}
			return _context.SahtakContactUs != null ? 
                          View(await _context.SahtakContactUs.ToListAsync()) :
                          Problem("Entity set 'ModelContext.SahtakContactUs'  is null.");
        }

        // GET: AdminDashboard/SahtakContactUs/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
			ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
			ViewData["FirstName"] = HttpContext.Session.GetString("FirstName");
			ViewData["LastName"] = HttpContext.Session.GetString("LastName");
			if (HttpContext.Session.GetInt32("UserID") == null)
			{
				return RedirectToAction("SignIn", "Account", new { area = "" });
			}
			if (id == null || _context.SahtakContactUs == null)
            {
                return NotFound();
            }

            var sahtakContactU = await _context.SahtakContactUs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sahtakContactU == null)
            {
                return NotFound();
            }

            return View(sahtakContactU);
        }

        // GET: AdminDashboard/SahtakContactUs/Create
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

        // POST: AdminDashboard/SahtakContactUs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,PhoneNumber,Email,Subject,Description")] SahtakContactU sahtakContactU)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sahtakContactU);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sahtakContactU);
        }

        // GET: AdminDashboard/SahtakContactUs/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
			ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
			ViewData["FirstName"] = HttpContext.Session.GetString("FirstName");
			ViewData["LastName"] = HttpContext.Session.GetString("LastName");
			if (HttpContext.Session.GetInt32("UserID") == null)
			{
				return RedirectToAction("SignIn", "Account", new { area = "" });
			}
			if (id == null || _context.SahtakContactUs == null)
            {
                return NotFound();
            }

            var sahtakContactU = await _context.SahtakContactUs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sahtakContactU == null)
            {
                return NotFound();
            }

            return View(sahtakContactU);
        }

        // POST: AdminDashboard/SahtakContactUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.SahtakContactUs == null)
            {
                return Problem("Entity set 'ModelContext.SahtakContactUs'  is null.");
            }
            var sahtakContactU = await _context.SahtakContactUs.FindAsync(id);
            if (sahtakContactU != null)
            {
                _context.SahtakContactUs.Remove(sahtakContactU);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SahtakContactUExists(decimal id)
        {
          return (_context.SahtakContactUs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
